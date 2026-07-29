import App from './App.svelte';
import { mount } from 'svelte';
import { getWebXPanel, runsInContainerApp } from '@crestron/ch5-webxpanel';

// Initialize WebXPanel for browser-based testing
const initializeWebXPanel = () => {
  try {
    const { isActive, WebXPanel, WebXPanelConfigParams } = getWebXPanel(!runsInContainerApp());
    
    // Only initialize if we're in a browser (not on touch panel)
    if (isActive) {
      const config: Partial<typeof WebXPanelConfigParams> = {
        host: "control-system.local", // Placeholder — set to your Crestron processor IP or hostname
        ipId: "0x03", // Touch panel connects to ID 3 (0x03)
        roomId: "",
        authToken: "",
        // Try without HTTPS first, then fallback to HTTPS if needed
        useHttps: false
      };

      console.log('Attempting WebXPanel connection to:', config.host);
      WebXPanel.initialize(config);
      console.log('WebXPanel initialized for browser testing');
      
      // Add connection status logging
      setTimeout(() => {
        console.log('WebXPanel status after 3 seconds');
      }, 3000);
    } else {
      console.log('WebXPanel not active - running on touch panel');
    }
    
    return true;
  } catch (error) {
    console.error('Failed to initialize WebXPanel:', error);
    return false;
  }
};

// Initialize WebXPanel before mounting app
initializeWebXPanel();

// Add debugging for contract file
if (window.location.hostname === 'localhost') {
  console.log('Running in development mode');
  console.log('Contract should be available at:', window.location.origin + '/config/contract.cse2j');
}

const app = mount(App, {
  target: document.getElementById('app')!
});

export default app;
