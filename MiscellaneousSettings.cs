using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using PolyTechFramework;
using static Vehicles;
using static Platforms;
using static SandboxInputField;

namespace MiscellaneousSettings
{
    [BepInPlugin("polytech.miscellaneous_settings", "Miscellaneous Settings Mod", "1.1.0")]
    [BepInProcess("Poly Bridge 2")]
    [BepInDependency(PolyTechMain.PluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    public class MiscellaneousSettings : PolyTechMod
    {
        
        public void Awake()
        {
            int order = 0;
            base.Config.Bind<bool>(MiscellaneousSettings.EnabledDef, false, new ConfigDescription("Enable/Disable", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.EnabledToggle = (ConfigEntry<bool>)base.Config[MiscellaneousSettings.EnabledDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MaxMassDef, 1000f, new ConfigDescription("Set Max Vehicle Mass", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MaxMass = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MaxMassDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinMassDef, 0.4f, new ConfigDescription("Set Min Vehicle Mass", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinMass = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinMassDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MaxSpeedDef, 100f, new ConfigDescription("Set Max Vehicle Speed", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MaxSpeed = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MaxSpeedDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinSpeedDef, 0f, new ConfigDescription("Set Min Vehicle Speed", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinSpeed = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinSpeedDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MaxAccelerationDef, 100f, new ConfigDescription("Set Max Vehicle Acceleration", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MaxAcceleration = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MaxAccelerationDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinAccelerationDef, 0f, new ConfigDescription("Set Min Vehicle Acceleration", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinAcceleration = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinAccelerationDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MaxHorsepowerDef, 100f, new ConfigDescription("Set Max Vehicle Horsepower", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MaxHorsepower = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MaxHorsepowerDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinHorsepowerDef, 0f, new ConfigDescription("Set Min Vehicle Horsepower", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinHorsepower = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinHorsepowerDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MaxBrakingIntensityDef, 100f, new ConfigDescription("Set Max Vehicle Braking Intensity", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MaxBrakingIntensity = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MaxBrakingIntensityDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinBrakingIntensityDef, 1f, new ConfigDescription("Set Min Vehicle Braking Intensity", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinBrakingIntensity = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinBrakingIntensityDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MaxShocksDef, 10f, new ConfigDescription("Set Max Shocks Multiplier", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MaxShocks = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MaxShocksDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinShocksDef, 0.1f, new ConfigDescription("Set Min Shocks Multiplier", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinShocks = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinShocksDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinPlatformWidthDef, 1f, new ConfigDescription("Set Min Platform Width", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinPlatformWidth = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinPlatformWidthDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinWaterHeightDef, 0.25f, new ConfigDescription("Set Min Water Height", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinWaterHeight = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinWaterHeightDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinWaterWidthDef, 0.05f, new ConfigDescription("Set Min Water Width", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinWaterWidth = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinWaterWidthDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MaxPillarHeightDef, 150, new ConfigDescription("Set Max Pillar Height", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MaxPillarHeight = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MaxPillarHeightDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinPillarHeightDef, 1f, new ConfigDescription("Set Min Pillar Height", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinPillarHeight = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinPillarHeightDef];
            order--;

            base.Config.Bind<float>(MiscellaneousSettings.MinFlagPoleHeightDef, 0.75f, new ConfigDescription("Set Min Flag Pole Height", null, new ConfigurationManagerAttributes { Order = order }));
            MiscellaneousSettings.MinFlagPoleHeight = (ConfigEntry<float>)base.Config[MiscellaneousSettings.MinFlagPoleHeightDef];
            order--;

            new Harmony("polytech.miscellaneous_settings").PatchAll(Assembly.GetExecutingAssembly());

            this.isCheat = false;
            this.isEnabled = MiscellaneousSettings.EnabledToggle.Value;

            PolyTechMain.registerMod(this);

            base.Config.SettingChanged += delegate(object o, SettingChangedEventArgs e)
			{
                onEnableDisable();
				MiscellaneousSettings.UpdateValuesIGuess();
			};
        }

        public void onEnableDisable()
		{
			this.isEnabled = MiscellaneousSettings.EnabledToggle.Value;
		}
        internal static void UpdateValuesIGuess()
        {
            bool flag = !MiscellaneousSettings.EnabledToggle.Value || !PolyTechMain.modEnabled.Value;
            if (flag)
            {
                Vehicles.MAX_MASS = 1000f;
                Vehicles.MIN_MASS = 0.4f;
                Vehicles.MAX_SPEED = 100f;
                Vehicles.MIN_SPEED = 0f;
                Vehicles.MAX_DESIRED_ACCELERATION = 100f;
                Vehicles.MIN_DESIRED_ACCELERATION = 0f;
                Vehicles.MAX_HORSEPOWER = 100f;
                Vehicles.MIN_HORSEPOWER = 0f;
                Vehicles.MAX_BREAKING_INTENSITY = 100f;
                Vehicles.MIN_BRAKING_INTENSITY = 1f;
                Vehicles.MAX_SHOCKS_MULTIPLIER = 10f;
                Vehicles.MIN_SHOCKS_MULTIPLIER = 0.1f;
                Platforms.MIN_WIDTH = 1f;
                WaterBlocks.MIN_HEIGHT = 0.25f;
                WaterBlocks.MIN_WIDTH = 0.05f;
                Pillars.MAX_HEIGHT = GameSettings.WorldMaxY() - 50f;
                Pillars.MIN_HEIGHT = 1f;
                VehicleStopTriggers.MIN_POLE_HEIGHT = 0.75f;
            }
            else
            {
                if (MiscellaneousSettings.MaxMass.Value > MiscellaneousSettings.MinMass.Value)
                {
                    Vehicles.MAX_MASS = MiscellaneousSettings.MaxMass.Value;
                    if (MiscellaneousSettings.MinMass.Value >= 0)
                    {
                        Vehicles.MIN_MASS = MiscellaneousSettings.MinMass.Value;
                    }
                    else
                    {
                        MiscellaneousSettings.MinMass.Value = 0;
                    }
                }
                if (MiscellaneousSettings.MaxSpeed.Value > MiscellaneousSettings.MinSpeed.Value)
                {
                    Vehicles.MAX_SPEED = MiscellaneousSettings.MaxSpeed.Value;
                    if (MiscellaneousSettings.MinSpeed.Value >= 0)
                    {
                        Vehicles.MIN_SPEED = MiscellaneousSettings.MinSpeed.Value;
                    }
                    else
                    {
                        MiscellaneousSettings.MinSpeed.Value = 0;
                    }
                }
                if (MiscellaneousSettings.MaxAcceleration.Value > MiscellaneousSettings.MinAcceleration.Value)
                {
                    Vehicles.MAX_DESIRED_ACCELERATION = MiscellaneousSettings.MaxAcceleration.Value;
                    if (MiscellaneousSettings.MinAcceleration.Value >= 0)
                    {
                        Vehicles.MIN_DESIRED_ACCELERATION = MiscellaneousSettings.MinAcceleration.Value;
                    }
                    else
                    {
                        MiscellaneousSettings.MinAcceleration.Value = 0;
                    }
                }
                if (MiscellaneousSettings.MaxHorsepower.Value > MiscellaneousSettings.MinHorsepower.Value)
                {
                    Vehicles.MAX_HORSEPOWER = MiscellaneousSettings.MaxHorsepower.Value;
                    if (MiscellaneousSettings.MinHorsepower.Value >= 0)
                    {
                        Vehicles.MIN_HORSEPOWER = MiscellaneousSettings.MinHorsepower.Value;
                    }
                    else
                    {
                        MiscellaneousSettings.MinHorsepower.Value = 0;
                    }
                }
                if (MiscellaneousSettings.MaxBrakingIntensity.Value > MiscellaneousSettings.MinBrakingIntensity.Value)
                {
                    Vehicles.MAX_BREAKING_INTENSITY = MiscellaneousSettings.MaxBrakingIntensity.Value;
                    if (MiscellaneousSettings.MinBrakingIntensity.Value >= 0)
                    {
                        Vehicles.MIN_BRAKING_INTENSITY = MiscellaneousSettings.MinBrakingIntensity.Value;
                    }
                    else
                    {
                        MiscellaneousSettings.MinBrakingIntensity.Value = 0;
                    }
                }
                if (MiscellaneousSettings.MaxShocks.Value > MiscellaneousSettings.MinShocks.Value)
                {
                    Vehicles.MAX_SHOCKS_MULTIPLIER = MiscellaneousSettings.MaxShocks.Value;
                    if (MiscellaneousSettings.MinShocks.Value >= 0)
                    {
                        Vehicles.MIN_SHOCKS_MULTIPLIER = MiscellaneousSettings.MinShocks.Value;
                    }
                    else
                    {
                        MiscellaneousSettings.MinShocks.Value = 0;
                    }
                }
                Platforms.MIN_WIDTH = MiscellaneousSettings.MinPlatformWidth.Value;
                WaterBlocks.MIN_HEIGHT = MiscellaneousSettings.MinWaterHeight.Value;
                WaterBlocks.MIN_WIDTH = MiscellaneousSettings.MinWaterWidth.Value;
                if (MiscellaneousSettings.MaxPillarHeight.Value > MiscellaneousSettings.MinPillarHeight.Value)
                {
                    Pillars.MAX_HEIGHT = MiscellaneousSettings.MaxPillarHeight.Value;
                    Pillars.MIN_HEIGHT = MiscellaneousSettings.MinPillarHeight.Value;
                }
                VehicleStopTriggers.MIN_POLE_HEIGHT = MiscellaneousSettings.MinFlagPoleHeight.Value;
            }
        }
        //Stuff at the end i guess
        public static ConfigDefinition EnabledDef = new ConfigDefinition("Enable/Disable", "Enable/Disable");
        public static ConfigEntry<bool> EnabledToggle;
        public static ConfigDefinition MaxMassDef = new ConfigDefinition("Vehicles", "Max Mass");
        public static ConfigEntry<float> MaxMass;
        public static ConfigDefinition MinMassDef = new ConfigDefinition("Vehicles", "Min Mass");
        public static ConfigEntry<float> MinMass;
        public static ConfigDefinition MaxSpeedDef = new ConfigDefinition("Vehicles", "Max Speed");
        public static ConfigEntry<float> MaxSpeed;
        public static ConfigDefinition MinSpeedDef = new ConfigDefinition("Vehicles", "Min Speed");
        public static ConfigEntry<float> MinSpeed;
        public static ConfigDefinition MaxAccelerationDef = new ConfigDefinition("Vehicles", "Max Acceleration");
        public static ConfigEntry<float> MaxAcceleration;
        public static ConfigDefinition MinAccelerationDef = new ConfigDefinition("Vehicles", "Min Acceleration");
        public static ConfigEntry<float> MinAcceleration;
        public static ConfigDefinition MaxHorsepowerDef = new ConfigDefinition("Vehicles", "Max Horsepower");
        public static ConfigEntry<float> MaxHorsepower;
        public static ConfigDefinition MinHorsepowerDef = new ConfigDefinition("Vehicles", "Min Horsepower");
        public static ConfigEntry<float> MinHorsepower;
        public static ConfigDefinition MaxBrakingIntensityDef = new ConfigDefinition("Vehicles", "Max Braking Intensity");
        public static ConfigEntry<float> MaxBrakingIntensity;
        public static ConfigDefinition MinBrakingIntensityDef = new ConfigDefinition("Vehicles", "Min Braking Intensity");
        public static ConfigEntry<float> MinBrakingIntensity;
        public static ConfigDefinition MaxShocksDef = new ConfigDefinition("Vehicles", "Max Shocks Multiplier");
        public static ConfigEntry<float> MaxShocks;
        public static ConfigDefinition MinShocksDef = new ConfigDefinition("Vehicles", "Min Shocks Multiplier");
        public static ConfigEntry<float> MinShocks;
        public static ConfigDefinition MinPlatformWidthDef = new ConfigDefinition("Platforms", "Min Platform Width");
        public static ConfigEntry<float> MinPlatformWidth;
        public static ConfigDefinition MinWaterHeightDef = new ConfigDefinition("Water Blocks", "Min Water Height");
        public static ConfigEntry<float> MinWaterHeight;
        public static ConfigDefinition MinWaterWidthDef = new ConfigDefinition("Water Blocks", "Min Water Width");
        public static ConfigEntry<float> MinWaterWidth;
        public static ConfigDefinition MaxPillarHeightDef = new ConfigDefinition("Pillars", "Max Pillar Height");
        public static ConfigEntry<float> MaxPillarHeight;
        public static ConfigDefinition MinPillarHeightDef = new ConfigDefinition("Pillars", "Min Pillar Height");
        public static ConfigEntry<float> MinPillarHeight;
        public static ConfigDefinition MinFlagPoleHeightDef = new ConfigDefinition("Flags", "Min Flag Pole Height");
        public static ConfigEntry<float> MinFlagPoleHeight;

        #pragma warning disable 0169, 0414, 0649
        internal sealed class ConfigurationManagerAttributes
        {
            /// <summary>
            /// Should the setting be shown as a percentage (only use with value range settings).
            /// </summary>
            public bool? ShowRangeAsPercent;

            /// <summary>
            /// Custom setting editor (OnGUI code that replaces the default editor provided by ConfigurationManager).
            /// See below for a deeper explanation. Using a custom drawer will cause many of the other fields to do nothing.
            /// </summary>
            public System.Action<BepInEx.Configuration.ConfigEntryBase> CustomDrawer;

            /// <summary>
            /// Show this setting in the settings screen at all? If false, don't show.
            /// </summary>
            public bool? Browsable;

            /// <summary>
            /// Category the setting is under. Null to be directly under the plugin.
            /// </summary>
            public string Category;

            /// <summary>
            /// If set, a "Default" button will be shown next to the setting to allow resetting to default.
            /// </summary>
            public object DefaultValue;

            /// <summary>
            /// Force the "Reset" button to not be displayed, even if a valid DefaultValue is available. 
            /// </summary>
            public bool? HideDefaultButton;

            /// <summary>
            /// Force the setting name to not be displayed. Should only be used with a <see cref="CustomDrawer"/> to get more space.
            /// Can be used together with <see cref="HideDefaultButton"/> to gain even more space.
            /// </summary>
            public bool? HideSettingName;

            /// <summary>
            /// Optional description shown when hovering over the setting.
            /// Not recommended, provide the description when creating the setting instead.
            /// </summary>
            public string Description;

            /// <summary>
            /// Name of the setting.
            /// </summary>
            public string DispName;

            /// <summary>
            /// Order of the setting on the settings list relative to other settings in a category.
            /// 0 by default, higher number is higher on the list.
            /// </summary>
            public int? Order;

            /// <summary>
            /// Only show the value, don't allow editing it.
            /// </summary>
            public bool? ReadOnly;

            /// <summary>
            /// If true, don't show the setting by default. User has to turn on showing advanced settings or search for it.
            /// </summary>
            public bool? IsAdvanced;

            /// <summary>
            /// Custom converter from setting type to string for the built-in editor textboxes.
            /// </summary>
            public System.Func<object, string> ObjToStr;

            /// <summary>
            /// Custom converter from string to setting type for the built-in editor textboxes.
            /// </summary>
            public System.Func<string, object> StrToObj;
        }
    }
}
