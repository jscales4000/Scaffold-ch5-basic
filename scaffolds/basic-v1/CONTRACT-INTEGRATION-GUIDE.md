# 🔗 Contract Integration Guide - Svelte Scaffold Touch Panel

## 📋 **Contract Overview**

**Contract Name**: `SvelteScaffoldTouchPanel`  
**Component**: `TouchPanelController`  
**Control Join ID**: 1  
**Status**: ✅ **Ready for Testing**

## 📁 **Generated Files**

All contract files are located in the scaffold root directory:
- **`SvelteScaffoldTouchPanel.cce`** - Contract Editor source file
- **`SvelteScaffoldTouchPanel.chd`** - SIMPL Windows symbol definition  
- **`SvelteScaffoldTouchPanel.ccz`** - Archive format for sharing

## 🎛️ **Signal Mapping**

### **Commands (Inputs)**
*These signals come FROM the touch panel TO the control system*

| Join | Type | Signal Name | UI Element | Description |
|------|------|-------------|------------|-------------|
| 1 | Digital | `VolumeUp` | Volume + Button | Momentary volume up press |
| 2 | Digital | `VolumeDown` | Volume - Button | Momentary volume down press |
| 3 | Digital | `MuteToggle` | Mute Button | Audio mute toggle |
| 4 | Digital | `PowerButton` | Power Button | System power press |
| 5 | Digital | `HelpButton` | Help Button | Help button press |
| 6 | Digital | `SettingsButton` | Settings Button | Settings button press |
| 1 | Analog | `SelectSource` | Source Buttons | Source selection (1-6) |

### **Feedbacks (Outputs)**
*These signals come FROM the control system TO the touch panel*

| Join | Type | Signal Name | UI Effect | Description |
|------|------|-------------|-----------|-------------|
| 1 | Digital | `IsMuted` | Red mute icon | Audio mute status indicator |
| 2 | Digital | `SystemPowered` | Power button state | System power status |
| 1 | Analog | `SelectedSource` | Red source highlight | Current active source (1-6) |
| 2 | Analog | `VolumeLevel` | Volume gauge fill | Current volume (0-65535) |

## 🔧 **Implementation Steps**

### **Phase 1: Contract Editor Validation** ✅ **READY**
1. Open Crestron Contract Editor
2. File → Open → Select `SvelteScaffoldTouchPanel.cce`
3. Verify all components and signals load correctly
4. Check signal names match UI requirements

### **Phase 2: SIMPL Windows Integration** ⏳ **NEXT**
1. Open SIMPL Windows
2. Create new program or open existing project
3. Database → User → Import CHD File → Select `SvelteScaffoldTouchPanel.chd`
4. Drag `TouchPanelController` symbol to workspace
5. Connect signals to your control logic

### **Phase 3: CH5 Signal Binding** ⏳ **PENDING**
1. Update Svelte components to use CH5 signal binding
2. Connect UI events to contract signals
3. Bind feedback signals to UI state changes

## 🎯 **Signal Implementation Examples**

### **Source Selection Logic (SIMPL)**
```
// When SelectSource analog changes:
SelectSource = 1      // PC selected
SelectSource = 2      // Laptop selected  
SelectSource = 3      // Airmedia selected
SelectSource = 4      // Doc Cam selected
SelectSource = 5      // Lectern HDMI selected
SelectSource = 6      // Floor Plate selected

// Send back to SelectedSource feedback:
SelectedSource = SelectSource  // Update UI highlight
```

### **Volume Control Logic (SIMPL)**
```
// Volume Up/Down pulses:
VolumeUp   → Volume_Control_Module.Up
VolumeDown → Volume_Control_Module.Down

// Volume feedback:
Volume_Control_Module.Level → VolumeLevel (Join 2, Analog)
```

### **Mute Logic (SIMPL)**
```
// Mute toggle:
MuteToggle → Toggle(MuteState)
MuteState  → IsMuted (Join 1, Digital)
```

## 🔗 **CH5 Integration (Future)**

### **Signal Binding in Svelte Components**
```typescript
// Example: Bind source selection
function selectSource(sourceId: number) {
  // Send analog command to control system
  CrComLib.publishEvent('a', '1.1', sourceId);
}

// Example: Subscribe to feedback
CrComLib.subscribeState('a', '2.1', (value) => {
  // Update UI when SelectedSource feedback changes
  selectedSourceFeedback = value; // 1-6 for different sources
});

CrComLib.subscribeState('d', '2.1', (value) => {
  // Update mute icon when IsMuted feedback changes
  isMutedFeedback = value;
});
```

## ✅ **Contract Verification Checklist**

### **Contract Editor (.cce file)**
- [ ] File opens without errors in Contract Editor
- [ ] TouchPanelController component visible
- [ ] 7 command signals present (6 digital + 1 analog)
- [ ] 4 feedback signals present (2 digital + 2 analog)
- [ ] All signal names match UI requirements
- [ ] Description under 100 characters

### **SIMPL Windows (.chd file)**
- [ ] CHD file imports successfully
- [ ] TouchPanelController symbol appears in database
- [ ] All input/output signals visible on symbol
- [ ] Signal join numbers match contract specification
- [ ] Program compiles without errors

### **UI Integration**
- [ ] CH5 signal binding implemented
- [ ] Command signals fire on button presses
- [ ] Feedback signals update UI states
- [ ] Volume gauge responds to analog feedback
- [ ] Source selection highlights work correctly

## 🚨 **Known Limitations**

1. **CHD Format**: May need refinement for full SIMPL Windows compatibility (workaround: use Contract Editor build process)
2. **CH5 Binding**: Requires additional implementation in Svelte components
3. **Testing**: Contract validation needed in actual Crestron development environment

## 🎯 **Next Steps**

1. **Test Contract Editor**: Validate `.cce` file opens and functions correctly
2. **SIMPL Windows Import**: Test `.chd` file import and symbol functionality  
3. **CH5 Integration**: Implement signal binding in Svelte components
4. **End-to-End Testing**: Full touch panel to control system workflow
5. **Documentation**: Update scaffold documentation with CH5 integration guide

---

## 📞 **Support**

For contract integration questions or issues:
- Reference: [Contract Testing Plan](../../../MCPServers/CrestronBrain%20MCP/CONTRACT_TESTING_PLAN.md)
- Known Working Examples: `test-contracts/` directory
- Contract Generator: CrestronBrain MCP system

**Status**: Contract generation complete - ready for Crestron tool validation! 🚀