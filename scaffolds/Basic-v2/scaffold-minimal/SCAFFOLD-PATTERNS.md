# CH5 Svelte Scaffold Patterns

## ✅ **Proven Working Configuration**

This document captures the exact patterns that work with Crestron CH5 and should be replicated in all scaffolds.

### 🔧 **Essential Build Configuration**

**vite.config.ts** - Critical plugins:
```typescript
import { viteSingleFile } from 'vite-plugin-singlefile';
import { viteStaticCopy } from 'vite-plugin-static-copy';

plugins: [
  svelte(),
  viteStaticCopy({
    targets: [{
      src: 'node_modules/@crestron/ch5-crcomlib/build_bundles/umd/cr-com-lib.js',
      dest: ''
    }]
  }),
  viteSingleFile(), // CRITICAL: Creates single HTML file for touch panels
]
```

**index.html** - Required CrComLib import:
```html
<!-- Import CrComLib BEFORE app scripts -->
<script src="./cr-com-lib.js"></script>
```

### 🎯 **Signal Connection Patterns**

**Digital Signals** (Buttons/Triggers):
```typescript
// Import from ch5-svelte
import { useDigital } from 'ch5-svelte';

// Create signal connection
const powerSignal = useDigital('MPC3201B.Power');

// Use in component
function handleClick() {
  powerSignal.pulse(); // For momentary actions
}

// Access feedback value  
$: isActive = powerSignal.value; // Reactive feedback (NO $ prefix!)
```

**Analog Signals** (Numbers/Levels):
```typescript
import { useAnalog } from 'ch5-svelte';

const volumeLevel = useAnalog("MPC3201B.Bargraph");
const sourceControl = useAnalog("MPC3201B.Source");

// Read feedback
$: volumePercent = (volumeLevel.value / 65535) * 100;

// Send commands  
function selectSource(id: number) {
  sourceControl.setValue(id);
}
```

**Serial Signals** (Text/Strings):
```typescript
import { useSerial } from 'ch5-svelte';

// For names/text that may change
let roomName = '';
let roomNameSubscriptionId: string | undefined;

onMount(() => {
  if (window.CrComLib && typeof window.CrComLib.subscribeState === 'function') {
    roomNameSubscriptionId = window.CrComLib.subscribeState(
      's',
      'MPC3201B.RoomName',
      (value: string) => {
        roomName = value || 'Default Name';
      }
    );
    
    // Get initial value
    const initialValue = window.CrComLib.getState('s', 'MPC3201B.RoomName');
    roomName = initialValue || 'Default Name';
  }
  
  // Cleanup
  return () => {
    if (window.CrComLib && roomNameSubscriptionId) {
      window.CrComLib.unsubscribeState('s', 'MPC3201B.RoomName', roomNameSubscriptionId);
    }
  };
});
```

### 📡 **WebXPanel Configuration**

**main.ts** - Robust initialization:
```typescript
import { getWebXPanel, runsInContainerApp } from '@crestron/ch5-webxpanel';

const initializeWebXPanel = () => {
  try {
    const { isActive, WebXPanel, WebXPanelConfigParams } = getWebXPanel(!runsInContainerApp());
    const config: Partial<typeof WebXPanelConfigParams> = {
      host: "control-system.local", // Replace with your control system IP
      ipId: "0x03",          // User's touch panel ID
      roomId: "",
      authToken: ""
    };

    if (isActive) {
      WebXPanel.initialize(config);
    }
    
    return true;
  } catch (error) {
    console.error('Failed to initialize WebXPanel:', error);
    return false;
  }
};
```

### 🏗️ **Contract Signal Reference**

**From MPC3201B Contract (working):**

**Digital Events (Commands):**
- `MPC3201B.Power` - System power toggle
- `MPC3201B.VolumeUp` - Volume increase
- `MPC3201B.VolumeDown` - Volume decrease  
- `MPC3201B.Mute` - Audio mute toggle

**Digital Feedback:**
- `MPC3201B.MuteFb` - Mute status (boolean)
- `MPC3201B.PowerIsOn` - Power status (boolean)

**Analog Events (Set Values):**
- `MPC3201B.Source` - Source selection (1-8)

**Analog Feedback:**
- `MPC3201B.Bargraph` - Volume level (0-65535)
- `MPC3201B.SourceFb` - Current source ID (1-8)
- `MPC3201B.SourceBtnCount` - Number of available sources

**String Feedback:**
- `MPC3201B.RoomName` - Room display name
- `MPC3201B.Source1Name` through `MPC3201B.Source8Name` - Source names
- `MPC3201B.HelpText` - Help content

### 🎨 **UI Design Patterns**

**Touch-Friendly Sizing:**
```css
button {
  min-height: 50px;    /* Touch target minimum */
  min-width: 80px;     /* Touch target minimum */
  font-size: 16px;     /* Readable text */
  padding: 12px 20px;  /* Comfortable spacing */
  border-radius: 8px;  /* Modern appearance */
  transition: all 0.2s ease; /* Smooth feedback */
}

button:hover {
  transform: translateY(-2px); /* Visual feedback */
}
```

**Responsive Grid:**
```css
.control-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 20px;
}

/* Touch panel specific */
@media (max-width: 1024px) {
  .control-grid {
    grid-template-columns: 1fr;
  }
}
```

### ⚡ **Key Success Factors**

1. **Single File Build** - `viteSingleFile()` is essential for touch panels
2. **Direct ch5-svelte Usage** - No custom wrappers, use library as designed
3. **Proper Error Handling** - WebXPanel initialization must handle failures gracefully
4. **Contract Consistency** - Always use same contract file across projects
5. **Touch Optimization** - Minimum 50px touch targets, clear visual feedback

### 🚀 **Scaffold Deployment Checklist**

- [ ] Uses `viteSingleFile()` plugin
- [ ] Includes `<script src="./cr-com-lib.js"></script>` in index.html
- [ ] WebXPanel config matches user's system (IP/ID)
- [ ] Contract files copied to `public/config/`
- [ ] All signals use exact naming from contract
- [ ] Touch-friendly button sizing (50px minimum)
- [ ] Error boundaries for CrComLib failures
- [ ] Build produces single HTML file under 200KB

This pattern creates a **107KB single-file** touch panel interface that loads reliably on Crestron hardware.