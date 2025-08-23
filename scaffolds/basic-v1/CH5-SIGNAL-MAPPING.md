# 🔗 CH5 Signal Mapping - Touch Panel to Control System

## 📡 **Signal Connections Fixed!**

The Svelte scaffold now has proper **CrComLib** signal binding integrated. Here's the exact mapping:

## 🎛️ **Commands (Touch Panel → Control System)**

| UI Element | Function | CH5 Signal | Join | Notes |
|------------|----------|------------|------|-------|
| **Volume Up Button** | `handleVolumeUp()` | `d:1.1` | Digital 1.1 | Momentary press (100ms) |
| **Volume Down Button** | `handleVolumeDown()` | `d:1.2` | Digital 1.2 | Momentary press (100ms) |
| **Mute Button** | `toggleMute()` | `d:1.3` | Digital 1.3 | Momentary press (100ms) |
| **Power Button** | `handlePowerButton()` | `d:1.4` | Digital 1.4 | Momentary press (100ms) |
| **Help Button** | `handleHelpButton()` | `d:1.5` | Digital 1.5 | Momentary press (100ms) |
| **Settings Button** | `handleSettingsButton()` | `d:1.6` | Digital 1.6 | Momentary press (100ms) |
| **Source Selection** | `selectSource(1-6)` | `a:1.1` | Analog 1.1 | Value 1-6 for different sources |

## 📊 **Feedbacks (Control System → Touch Panel)**

| UI Effect | CH5 Signal | Join | Function | Notes |
|-----------|------------|------|----------|-------|
| **Mute Icon (Red)** | `d:2.1` | Digital 2.1 | Updates `isMuted` | Shows red icon when true |
| **System Power State** | `d:2.2` | Digital 2.2 | Console log | Power status indicator |
| **Source Highlight (Red)** | `a:2.1` | Analog 2.1 | Updates `currentSource` | Value 1-6 highlights active source |
| **Volume Gauge** | `a:2.2` | Analog 2.2 | Updates `currentVolume` | 0-65535 → 0-100% conversion |

## 🔧 **SIMPL Windows Programming**

Connect your contract symbol joins to your logic:

### **Digital Inputs (From Touch Panel)**
```
TouchPanelController.VolumeUp      → Volume_Module.Up
TouchPanelController.VolumeDown    → Volume_Module.Down  
TouchPanelController.MuteToggle    → Toggle(Mute_State)
TouchPanelController.PowerButton   → System_Power_Logic
TouchPanelController.HelpButton    → Help_Display_Logic
TouchPanelController.SettingsButton → Settings_Menu_Logic
```

### **Analog Input (From Touch Panel)**
```
TouchPanelController.SelectSource  → Source_Selection_Logic
  • Value 1 = PC
  • Value 2 = Laptop  
  • Value 3 = Airmedia
  • Value 4 = Doc Cam
  • Value 5 = Lectern HDMI
  • Value 6 = Floor Plate
```

### **Digital Outputs (To Touch Panel)**
```
Mute_State                    → TouchPanelController.IsMuted
System_Power_State           → TouchPanelController.SystemPowered
```

### **Analog Outputs (To Touch Panel)**
```
Selected_Source_Number       → TouchPanelController.SelectedSource
Volume_Level_0_65535        → TouchPanelController.VolumeLevel
```

## ✅ **Key Features Added:**

1. **Proper CH5 Signal Binding**: All buttons now use `CrComLib.publishEvent()`
2. **Feedback Subscriptions**: UI updates automatically from control system
3. **Momentary Button Behavior**: Digital signals pulse for 100ms then reset
4. **Source Selection**: Single analog signal (1-6) instead of 6 separate digitals
5. **Volume Conversion**: Automatic scaling from Crestron's 0-65535 to UI's 0-100%
6. **Safety Checks**: Code checks if `CrComLib` exists before using it

## 🚀 **Testing the Connection:**

1. **Load the new CH5Z** on your touch panel
2. **Connect the signals** in SIMPL Windows as shown above  
3. **Test each function**:
   - Press volume buttons → Check digital pulses in SIMPL
   - Press source buttons → Check analog value changes
   - Send feedback from SIMPL → Watch UI update

The touch panel should now communicate properly with your control system! 🎉

## 📝 **Updated Files:**
- ✅ `SvelteScaffoldTouchPanel.ch5z` - New CH5Z with signal binding
- ✅ `src/App.svelte` - Added CrComLib integration  
- ✅ `src/components/shared/Header.svelte` - Connected Help/Settings buttons
- ✅ `src/components/shared/Footer.svelte` - Connected Power button