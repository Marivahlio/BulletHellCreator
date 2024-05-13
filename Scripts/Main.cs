using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Godot;

public partial class Main : Node
{
	public enum BulletDistributionTypes {Even, Custom}

	[ExportGroup("General References")] 
	[Export] public Node2D Origin;
	[Export] private BulletPattern BulletPattern;
	[Export] private VBoxContainer[] TextContainers; 	// Index = tab order
	[Export] private VBoxContainer[] InputContainers; // Index = tab order
	[Export] private TabContainer MainTabContainer;
	[Export] private Control Filler;
	[Export] private FileDialog SaveFileDialog;
	[Export] private FileDialog LoadFileDialog;

	[ExportGroup("UI Prefabs")] 
	[Export] private PackedScene GenericTextScene;
	[Export] private PackedScene IntInputScene;
	[Export] private PackedScene FloatInputScene;
	[Export] private PackedScene BoolInputScene;
	[Export] private PackedScene Vector2InputScene;
	[Export] private PackedScene ColorInputScene;
	[Export] private PackedScene EnumInputScene;

	private List<BaseInput> InputItems = new();
	private bool UpdatingValues = false;

	public override void _Ready()
	{
		InstantiateData();
		UpdateDataValue();
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.R))
		{
			UpdateDataValue();
			BulletPattern.Restart();
		}
		
		MoveOrigin();
	}

	public void UpdateDataValue()
	{
		if (UpdatingValues)
		{	
			return; 
		}

		UpdatingValues = true;

		foreach (BaseInput inputObj in InputItems)
		{
			inputObj.UpdateDataValue();
		}

		UpdatingValues = false;
	}

	public void UpdateInputValue()
	{
		if (UpdatingValues)
		{	
			return; 
		}

		UpdatingValues = true;
		
		foreach (BaseInput inputObj in InputItems)
		{
			inputObj.UpdateInputValue();
		}

		UpdatingValues = false;
	}

	public void ShowSavePatternDataDialog()
	{
		SaveFileDialog.CurrentDir = "res://Saved/BulletPatterns";
		SaveFileDialog.Show();
	}

	public void ShowLoadPatternDataDialog()
	{
		LoadFileDialog.CurrentDir = "res://Saved/BulletPatterns";
		LoadFileDialog.Show();
	}

	public void SavePatternData()
	{
		BulletPattern.GetPatternData().SaveData(SaveFileDialog.CurrentDir + "/" + SaveFileDialog.CurrentFile);
	}

	public void LoadPatternData(string pFilePath)
	{
		GD.Print("user selected file to load at path: " + pFilePath);
		BulletPatternData LoadedData = ResourceLoader.Load<BulletPatternData>(pFilePath);
		BulletPattern.GetPatternData().LoadData(LoadedData);
		UpdateInputValue();
		BulletPattern.Restart();
	}

	private void InstantiateData()
	{
		BulletPatternData patternData = BulletPattern.GetPatternData();

		// Pattern Settings Tab
		MainTabContainer.CurrentTab = 0;
		CreateIntInput(																			"Bullets Per Burst",  patternData.GetBulletsPerBurst, patternData.SetBulletsPerBurst, 1, 1, 50, 1);
		CreateIntInput(																			"Burst Amount", 			patternData.GetBurstAmount, patternData.SetBurstAmount, 3, 1, 50, 1);
		CreateFloatInput(																		"Burst Interval", 		patternData.GetBurstInterval, patternData.SetBurstInterval, 0.1f, 0.01f, 10f, 0.01f);
		CreateBoolInput(																		"Looping", 						patternData.GetLooping, patternData.SetLooping, true);
		CreateFloatInput(																		"Loop Delay", 				patternData.GetLoopDelay, patternData.SetLoopDelay, 0, 0, 10, 0.01f);

		// Bullet Settings Tab
		MainTabContainer.CurrentTab = 1;
		CreateVector2Input(																	"Start Velocity", 		patternData.GetStartVelocity, patternData.SetStartVelocity, 700, 0, -10000, -10000, 10000, 10000, 0.01f, 0.01f);
		CreateFloatInput(																		"Lifetime", 					patternData.GetLifeTime, patternData.SetLifeTime, 1.0f, 0.01f, 10f, 0.01f);
		CreateVector2Input(																	"Scale", 							patternData.GetScale, patternData.SetScale, 2, 2, 0.05f, 0.05f, 10, 10, 0.01f, 0.01f);
		CreateColorInput(																		"Color", 							patternData.GetColor, patternData.SetColor, new Color(1, 1, 1, 1));
		CreateFloatInput(																		"Torque", 						patternData.GetTorque, patternData.SetTorque, 0, -360f, 360f, 0.01f);
		EnumInput DistributionInput = CreateEnumInput(			"Distribution", 			patternData.GetBulletDistribution, patternData.SetBulletDistribution, typeof(BulletDistributionTypes));
		CreateFloatInput(																		"Seperation", 				patternData.GetCustomBulletDistributionDistance, patternData.SetCustomBulletDistributionDistance, 45, -360, 360, 0.01f, new Condition(DistributionInput, 1));

		// Show first tab by default
		MainTabContainer.CurrentTab = 0;

		UpdateDataValue();
	}

	private void MoveOrigin()
	{
		Origin.GlobalPosition = Filler.GetScreenPosition() + Filler.GetGlobalRect().Size/2;
		Filler.CustomMinimumSize = new Vector2(GetViewport().GetVisibleRect().Size.X / 3 * 2, Filler.Size.Y);
	}

	// the event subscription used in ConnectSignals expect vars to be given and won't work otherwhise, but we don't care about those
	private void UpdateDataValueRedirect(double pFakeInput){UpdateDataValue();}
	private void UpdateDataValueRedirect(long pFakeInput){UpdateDataValue();}
	private void UpdateDataValueRedirect(bool pFakeInput){UpdateDataValue();}
	private void UpdateDataValueRedirect(Color pFakeInput){UpdateDataValue();}

	private void CreateIntInput (string pDisplayName, Func<int> pLinkedGetter, Action<int> pLinkedSetter, int pDefaultValue, int pMinValue, int pMaxValue, int pStepSize, ConditionI pCondition = null)
	{
		IntInput InputObj = (IntInput)IntInputScene.Instantiate();
		InputObj.SetData(pLinkedGetter, pLinkedSetter, pDefaultValue, pMinValue, pMaxValue, pStepSize);
		InputObj.GetInputItem().ValueChanged += UpdateDataValueRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateFloatInput (string pDisplayName, Func<float> pLinkedGetter, Action<float> pLinkedSetter, float pDefaultValue, float pMinValue, float pMaxValue, float pStepSize, ConditionI pCondition = null)
	{
		FloatInput InputObj = (FloatInput)FloatInputScene.Instantiate();
		InputObj.SetData(pLinkedGetter, pLinkedSetter, pDefaultValue, pMinValue, pMaxValue, pStepSize);
		InputObj.GetInputItem().ValueChanged += UpdateDataValueRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateBoolInput (string pDisplayName, Func<bool> pLinkedGetter, Action<bool> pLinkedSetter, bool pDefaultValue, ConditionI pCondition = null)
	{
		BoolInput InputObj = (BoolInput)BoolInputScene.Instantiate();
		InputObj.SetData(pLinkedGetter, pLinkedSetter, pDefaultValue);
		InputObj.GetInputItem().Toggled += UpdateDataValueRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateVector2Input (string pDisplayName, Func<Vector2> pLinkedGetter, Action<float, float> pLinkedSetter, float pDefaultValueX, float pDefaultValueY, float pMinValueX, float pMinValueY, float pMaxValueX, float pMaxValueY, float pStepSizeX, float pStepSizeY, ConditionI pCondition = null)
	{
		Vector2Input InputObj = (Vector2Input)Vector2InputScene.Instantiate();
		InputObj.SetData(pLinkedGetter, pLinkedSetter, pDefaultValueX, pDefaultValueY, pMinValueX, pMinValueY, pMaxValueX, pMaxValueY, pStepSizeX, pStepSizeY);
		InputObj.GetInputItemX().ValueChanged += UpdateDataValueRedirect;
		InputObj.GetInputItemY().ValueChanged += UpdateDataValueRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateColorInput (string pDisplayName, Func<Color> pLinkedGetter, Action<Color> pLinkedSetter, Color pDefaultValue, ConditionI pCondition = null)
	{
		ColorInput InputObj = (ColorInput)ColorInputScene.Instantiate();
		InputObj.SetData(pLinkedGetter, pLinkedSetter, pDefaultValue);
		InputObj.GetInputItem().ColorChanged += UpdateDataValueRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private EnumInput CreateEnumInput (string pDisplayName, Func<int> pLinkedGetter, Action<int> pLinkedSetter, Type pEnum, ConditionI pCondition = null)
	{
		EnumInput InputObj = (EnumInput)EnumInputScene.Instantiate();
		InputObj.SetData(pLinkedGetter, pLinkedSetter, pEnum);
		InputObj.GetInputItem().ItemSelected += UpdateDataValueRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}

		return InputObj;
	}

	private GenericText CreateGenericTextObject(string pDisplayName)
	{
		GenericText TextObj = (GenericText)GenericTextScene.Instantiate();
		TextObj.SetData(pDisplayName);
		TextContainers[MainTabContainer.CurrentTab].AddChild(TextObj);

		return TextObj;
	}
}

public struct Condition : ConditionI
{
	public Condition(EnumInput pEnumInput, int pValue) { LinkedEnumInput = pEnumInput; EnumValue = pValue;}

	public EnumInput LinkedEnumInput {get; set;}
	public int EnumValue {get; set;}
}

public interface ConditionI 
{
	EnumInput LinkedEnumInput {get; set;}
	int EnumValue {get; set;}
}