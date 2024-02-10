using System;
using UnityEngine;
using Object = System.Object;

[RequireComponent(typeof(InventoryComponent))]
public class FurnaceCC : BuildableCC, ICreateDialogData {
    private float _fuelSecondsLeft, _smeltingProgress;
    private InventorySlot _fuelSlot, _smeltingSlot;

    public InventorySlot Fuel => _fuelSlot;
    public InventorySlot Smelting => _smeltingSlot;

    public Action<ComponentsContainer> OnTriggerUpdateView;

    public Resource GetCurrentRecipeResult => SmeltingRecipe.GetRecipe(Smelting.Resource).Result;

    protected override void Awake() {
        base.Awake();
        _fuelSlot = new InventorySlot();
        _smeltingSlot = new InventorySlot();
    }

    public DialogDataBase GetDialogData(Object obj = null) {
        Player player = obj as Player;
        InventoryComponent invComponent = GetComponent<InventoryComponent>();
        return new FurnaceInteractionDialogData() {
            Furnace = this,
            Player = player.GetInventory(),
            Chest = invComponent.Inventory
        };
    }

    public void Update() {
        bool needToTrigger = false;
        if (_fuelSecondsLeft <= 0) {
            _fuelSecondsLeft = 0;
            if (!_smeltingSlot.IsEmpty && !_fuelSlot.IsEmpty) {
                _fuelSecondsLeft += SmeltingRecipe.GetFuelAmount(_fuelSlot.Resource);
                _fuelSlot.RemoveResource(_fuelSlot.Resource.One);
                needToTrigger = true;
            }
        }

        if (_fuelSecondsLeft > 0) {
            _fuelSecondsLeft -= Time.deltaTime;
            needToTrigger = true;
            
            if (!_smeltingSlot.IsEmpty) {
                SmeltingRecipe recipe = SmeltingRecipe.GetRecipe(_smeltingSlot.Resource);
                _smeltingProgress += Time.deltaTime;
                if (_smeltingProgress >= recipe.TimeToSmelt) {
                    _smeltingSlot.RemoveResource(_smeltingSlot.Resource.One);
                    InventoryComponent invComponent = GetComponent<InventoryComponent>();
                    invComponent.Inventory.AddResource(recipe.Result);
                    _smeltingProgress = 0;
                }
            } else {
                _smeltingProgress = 0;
            }
        }

       

        if (needToTrigger) {
            TriggerUpdateView();
        }
    }

    private void TriggerUpdateView() {
        OnTriggerUpdateView?.Invoke(this);
    }
}

public struct SmeltingRecipe {
    public Resource Result;
    public float TimeToSmelt;

    public static SmeltingRecipe Empty => new SmeltingRecipe() {
        Result = Resource.Empty,
        TimeToSmelt = 0
    };

    public static SmeltingRecipe GetRecipe(Resource ingridient) {
        switch (ingridient.Type) {
            case ResourceType.IronOre:
                return new SmeltingRecipe() {
                    Result = new Resource() {
                        Type = ResourceType.IronIngot,
                        Amount = 1
                    },
                    TimeToSmelt = 3
                };
        }

        return SmeltingRecipe.Empty;
    }

    public static float GetFuelAmount(Resource res) {
        return res.Type switch {
            ResourceType.Wood => 5,
            ResourceType.Fiber => 1,
            _ => 0
        };
    }
}