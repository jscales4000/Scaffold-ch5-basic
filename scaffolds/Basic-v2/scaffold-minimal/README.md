# CH5 Svelte Minimal Scaffold

## 🎯 **Purpose**

This is a **minimal, proven scaffold** for creating Crestron CH5 touch panel interfaces using Svelte. It contains only essential controls and uses **exact working patterns** from the reference project.

## ✅ **What's Included**

### **Essential Controls**
- ⚡ **Power Control** - System power toggle using `MPC3201B.Power`
- 🔊 **Volume Control** - Up/Down/Mute with visual feedback bar
- 📱 **Source Selection** - 6 sources (PC, Laptop, Airmedia, Doc Cam, Lectern HDMI, Floor Plate)
- 📡 **Contract Integration** - Uses MPC3201B contract signals

### **Technical Features** 
- **Single File Build** - 107KB optimized for touch panels
- **Touch-Friendly UI** - 50px+ touch targets, responsive design
- **Error Resilience** - Graceful handling of connection failures
- **Named Signals** - Uses descriptive names, not join numbers

## 🚀 **Quick Start**

```bash
# Install dependencies
npm install

# Development mode
npm run dev

# Build for production
npm run build

# Create CH5Z package
npm run CH5Archive
```

## 📁 **Project Structure**

```
scaffold-minimal/
├── src/
│   ├── App.svelte          # Main application component
│   └── main.ts             # Entry point with WebXPanel config
├── public/config/          # Contract files
│   ├── contract.cse2j      # Signal mapping
│   └── contract.chd        # Contract definition
├── dist/                   # Build output (single HTML file)
├── package.json            # Dependencies & scripts  
├── vite.config.ts         # Build configuration
└── SCAFFOLD-PATTERNS.md   # Exact working patterns
```

## 🔧 **Configuration**

### **WebXPanel Settings**
Update in `src/main.ts`:
```typescript
const config = {
  host: "control-system.local",  // Replace with your control system IP
  ipId: "0x03",           // Your touch panel ID
  roomId: "",
  authToken: ""
};
```

### **Contract Signals Used**
- `MPC3201B.Power` - Power toggle
- `MPC3201B.VolumeUp/VolumeDown` - Volume control
- `MPC3201B.Mute` - Mute toggle
- `MPC3201B.Source` - Source selection (1-6)
- `MPC3201B.Bargraph` - Volume feedback (0-65535)
- `MPC3201B.SourceFb` - Current source feedback
- `MPC3201B.Source1Name` - `MPC3201B.Source6Name` - Source names

## 📈 **Expanding the Scaffold**

### **Adding New Controls**
1. Identify signal from contract (`public/config/contract.cse2j`)
2. Import appropriate hook: `useDigital`, `useAnalog`, or `useSerial`  
3. Follow patterns in `SCAFFOLD-PATTERNS.md`
4. Add UI component with touch-friendly sizing

### **Example: Adding Help Button**
```typescript
// In App.svelte <script>
const helpSignal = useDigital('MPC3201B.Help');
const helpTextSignal = useSerial('MPC3201B.HelpText');

function showHelp() {
  helpSignal.pulse();
  alert(helpTextSignal.value || 'Help information');
}

// In template
<button class="help-btn" on:click={showHelp}>Help</button>
```

### **Available Contract Signals**

From the MPC3201B contract, you can add:

**More Digital Controls:**
- `MPC3201B.ScreenUp/ScreenDown` - Projector screen
- `Settings.GuiMicActivate` - Microphone control  
- `Airmedia.Power` - Airmedia control

**More Analog Controls:**
- `MPC3201B.PowerState` - Power state feedback
- `Settings.S1SetIconID` - Custom source icons

**More String Feedback:**
- `MPC3201B.LampHours` - Projector lamp hours
- `Airmedia.ConnectionAddress` - Airmedia connection info

## 🎨 **UI Guidelines**

### **Touch Panel Optimization**
- **Minimum Touch Target**: 50px x 50px
- **Readable Text**: 16px minimum font size
- **Visual Feedback**: Hover effects, active states
- **Responsive**: Works on 7", 10", and 15" panels

### **Design Principles**
- **Clean Layout**: Card-based sections
- **Clear Hierarchy**: Headers, grouped controls
- **Status Feedback**: Visual confirmation of actions
- **Professional**: Corporate-friendly appearance

## 🔧 **Troubleshooting**

### **Black Screen Issues**
- Check WebXPanel IP/ID configuration
- Verify contract files in `public/config/`
- Ensure CrComLib script loads before app

### **Signal Issues**  
- Compare signal names with `contract.cse2j`
- Check console for connection errors
- Verify control system is online

### **Build Issues**
- Ensure `viteSingleFile()` plugin is enabled
- Check all dependencies are installed
- Verify TypeScript configurations

## 📦 **Deployment**

1. **Build**: `npm run build`
2. **Archive**: `npm run CH5Archive` 
3. **Upload**: Load `my-ch5-ui.ch5z` to touch panel
4. **Configure**: Set control system IP/ID to match WebXPanel config

## 🔄 **Creating New Scaffolds**

1. **Copy** this entire `scaffold-minimal` directory
2. **Rename** the project in `package.json`
3. **Update** WebXPanel configuration for target system  
4. **Add Features** following patterns in `SCAFFOLD-PATTERNS.md`
5. **Test** build and CH5Z creation
6. **Document** new features added

This scaffold provides a solid foundation that can be systematically expanded while maintaining the proven working patterns.