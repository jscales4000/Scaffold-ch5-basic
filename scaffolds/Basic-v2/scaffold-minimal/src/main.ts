import { mount } from 'svelte';
import App from './App.svelte';
import { getWebXPanel, runsInContainerApp } from '@crestron/ch5-webxpanel';

// Initialize WebXPanel with error handling
const initializeWebXPanel = () => {
  try {
    const { isActive, WebXPanel, WebXPanelConfigParams } = getWebXPanel(!runsInContainerApp());
    const config: Partial<typeof WebXPanelConfigParams> = {
      host: "192.168.2.153",
      ipId: "0x03",
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

// Main initialization
const initApp = async () => {
  // Initialize WebXPanel
  const xpanelInitialized = initializeWebXPanel();
  
  if (!xpanelInitialized) {
    console.warn('WebXPanel initialization failed, continuing in offline mode');
  }

  try {
    // Mount the app with error boundary
    const app = mount(App, {
      target: document.getElementById('app')!,
    });

    return app;
  } catch (error) {
    console.error('Failed to mount application:', error);
    // Render error fallback UI
    document.getElementById('app')!.innerHTML = `
      <div style="padding: 2rem; color: white; background: #0C234B; height: 100vh;">
        <h1>Application Error</h1>
        <p>${error instanceof Error ? error.message : 'An unknown error occurred'}</p>
        <p>Please refresh the page or contact support if the problem persists.</p>
      </div>
    `;
    throw error;
  }
};

// Start the application
initApp().catch(console.error);

export default initApp;