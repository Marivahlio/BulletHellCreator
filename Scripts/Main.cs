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

	[ExportGroup("UI Prefabs")] 
	[Export] private PackedScene GenericTextScene;
	[Export] private PackedScene IntInputScene;
	[Export] private PackedScene FloatInputScene;
	[Export] private PackedScene BoolInputScene;
	[Export] private PackedScene Vector2InputScene;
	[Export] private PackedScene ColorInputScene;
	[Export] private PackedScene EnumInputScene;

	private List<BaseInput> InputItems = new();

	public override void _Ready()
	{
		InstantiateData();
		UpdatePatternData();
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.R))
		{
			UpdatePatternData();
			BulletPattern.Restart();
		}
		
		MoveOrigin();
	}

	public void UpdatePatternData()
	{
		foreach (BaseInput inputObj in InputItems)
		{
			inputObj.UpdateValue();
		}
	}

	private void InstantiateData()
	{
		// Pattern Settings Tab
		MainTabContainer.CurrentTab = 0;
		CreateIntInput(																			"Bullets Per Burst", 	BulletPattern.GetPatternData().SetBulletsPerBurst, 1, 1, 50, 1);
		CreateIntInput(																			"Burst Amount", 			BulletPattern.GetPatternData().SetBurstAmount, 3, 1, 50, 1);
		CreateFloatInput(																		"Burst Interval", 		BulletPattern.GetPatternData().SetBurstInterval, 0.1f, 0.01f, 10f, 0.01f);
		CreateBoolInput(																		"Looping", 						BulletPattern.GetPatternData().SetLooping, true);
		CreateFloatInput(																		"Loop Delay", 				BulletPattern.GetPatternData().SetLoopDelay, 0, 0, 10, 0.01f);

		// Bullet Settings Tab
		MainTabContainer.CurrentTab = 1;
		CreateVector2Input(																	"Start Velocity", 		BulletPattern.GetPatternData().SetStartVelocity, 700, 0, -10000, -10000, 10000, 10000, 0.01f, 0.01f);
		CreateFloatInput(																		"Lifetime", 					BulletPattern.GetPatternData().SetLifeTime, 1.0f, 0.01f, 20f, 0.01f);
		CreateVector2Input(																	"Scale", 							BulletPattern.GetPatternData().SetScale, 2, 2, 0.05f, 0.05f, 10, 10, 0.01f, 0.01f);
		CreateColorInput(																		"Color", 							BulletPattern.GetPatternData().SetColor, new Color(1, 1, 1, 1));
		CreateFloatInput(																		"Torque", 						BulletPattern.GetPatternData().SetTorque, 0, -360f, 360f, 0.01f);
		EnumInput DistributionInput = CreateEnumInput(			"Distribution", 			BulletPattern.GetPatternData().SetBulletDistribution, typeof(BulletDistributionTypes));
		CreateFloatInput(																		"Seperation", 				BulletPattern.GetPatternData().SetCustomBulletDistributionDistance, 45, -360, 360, 0.01f, new Condition(DistributionInput, 1));

		// Show first tab by default
		MainTabContainer.CurrentTab = 0;

		UpdatePatternData();
	}

	private void MoveOrigin()
	{
		Origin.GlobalPosition = Filler.GetScreenPosition() + Filler.GetGlobalRect().Size/2;
		Filler.CustomMinimumSize = new Vector2(GetViewport().GetVisibleRect().Size.X / 3 * 2, Filler.Size.Y);
	}

	// the event subscription used in ConnectSignals expect vars to be given and won't work otherwhise, but we don't care about those
	private void UpdatePatternDataRedirect(double pFakeInput){UpdatePatternData();}
	private void UpdatePatternDataRedirect(long pFakeInput){UpdatePatternData();}
	private void UpdatePatternDataRedirect(bool pFakeInput){UpdatePatternData();}
	private void UpdatePatternDataRedirect(Color pFakeInput){UpdatePatternData();}

	private void CreateIntInput (string pDisplayName, Action<int> pLinkedSetter, int pDefaultValue, int pMinValue, int pMaxValue, int pStepSize, ConditionI pCondition = null)
	{
		IntInput InputObj = (IntInput)IntInputScene.Instantiate();
		InputObj.SetData(pLinkedSetter, pDefaultValue, pMinValue, pMaxValue, pStepSize);
		InputObj.GetInputItem().ValueChanged += UpdatePatternDataRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateFloatInput (string pDisplayName, Action<float> pLinkedSetter, float pDefaultValue, float pMinValue, float pMaxValue, float pStepSize, ConditionI pCondition = null)
	{
		FloatInput InputObj = (FloatInput)FloatInputScene.Instantiate();
		InputObj.SetData(pLinkedSetter, pDefaultValue, pMinValue, pMaxValue, pStepSize);
		InputObj.GetInputItem().ValueChanged += UpdatePatternDataRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateBoolInput (string pDisplayName, Action<bool> pLinkedSetter, bool pDefaultValue, ConditionI pCondition = null)
	{
		BoolInput InputObj = (BoolInput)BoolInputScene.Instantiate();
		InputObj.SetData(pLinkedSetter, pDefaultValue);
		InputObj.GetInputItem().Toggled += UpdatePatternDataRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateVector2Input (string pDisplayName, Action<float, float> pLinkedSetter, float pDefaultValueX, float pDefaultValueY, float pMinValueX, float pMinValueY, float pMaxValueX, float pMaxValueY, float pStepSizeX, float pStepSizeY, ConditionI pCondition = null)
	{
		Vector2Input InputObj = (Vector2Input)Vector2InputScene.Instantiate();
		InputObj.SetData(pLinkedSetter, pDefaultValueX, pDefaultValueY, pMinValueX, pMinValueY, pMaxValueX, pMaxValueY, pStepSizeX, pStepSizeY);
		InputObj.GetInputItemX().ValueChanged += UpdatePatternDataRedirect;
		InputObj.GetInputItemY().ValueChanged += UpdatePatternDataRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private void CreateColorInput (string pDisplayName, Action<Color> pLinkedSetter, Color pDefaultValue, ConditionI pCondition = null)
	{
		ColorInput InputObj = (ColorInput)ColorInputScene.Instantiate();
		InputObj.SetData(pLinkedSetter, pDefaultValue);
		InputObj.GetInputItem().ColorChanged += UpdatePatternDataRedirect;
		InputItems.Add(InputObj);
		InputContainers[MainTabContainer.CurrentTab].AddChild(InputObj);

		InputObj.SetLinkedTextObject(CreateGenericTextObject(pDisplayName));

		if (pCondition != null)
		{
			InputObj.SetCondition(pCondition);
		}
	}

	private EnumInput CreateEnumInput (string pDisplayName, Action<int> pLinkedSetter, Type pEnum, ConditionI pCondition = null)
	{
		EnumInput InputObj = (EnumInput)EnumInputScene.Instantiate();
		InputObj.SetData(pLinkedSetter, pEnum);
		InputObj.GetInputItem().ItemSelected += UpdatePatternDataRedirect;
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