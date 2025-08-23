<script lang="ts">
  /**
   * Minimal CH5 Scaffold - Essential Controls Only
   * Uses proven working patterns from the reference project
   */
  
  import { useAnalog, useDigital, useSerial } from 'ch5-svelte';
  import { onMount } from 'svelte';

  // Room name from contract
  let roomName = '';
  let roomNameSubscriptionId: string | undefined;

  // Essential signals using EXACT working patterns
  const powerSignal = useDigital('MPC3201B.Power');
  
  // Volume controls
  const volumeUpSignal = useDigital("MPC3201B.VolumeUp");
  const volumeDownSignal = useDigital("MPC3201B.VolumeDown");
  const muteToggleSignal = useDigital("MPC3201B.Mute");
  
  // Volume feedback
  const volumeFeedbackSignal = useAnalog("MPC3201B.Bargraph");
  const muteFeedbackSignal = useDigital("MPC3201B.MuteFb");
  
  // Source controls  
  const sourceSignal = useAnalog("MPC3201B.Source");
  const feedbackSignal = useAnalog("MPC3201B.SourceFb");

  // Source names (1-6 as specified)
  let sourceNames: string[] = ['PC', 'Laptop', 'Airmedia', 'Doc Cam', 'Lectern HDMI', 'Floor Plate'];
  let sourceNameSubscriptions: string[] = [];

  onMount(() => {
    // Room name subscription (exact pattern from working project)
    if (window.CrComLib && typeof window.CrComLib.subscribeState === 'function') {
      roomNameSubscriptionId = window.CrComLib.subscribeState(
        's',
        'MPC3201B.RoomName',
        (value: string) => {
          roomName = value || 'CH5 Touch Panel';
        }
      );

      // Get initial room name value
      const initialRoomName = window.CrComLib.getState('s', 'MPC3201B.RoomName');
      roomName = initialRoomName || 'CH5 Touch Panel';

      // Subscribe to source names
      for (let i = 1; i <= 6; i++) {
        const signalName = `MPC3201B.Source${i}Name`;
        const subscriptionId = window.CrComLib.subscribeState(
          's',
          signalName,
          (value: string) => {
            if (value) {
              sourceNames[i - 1] = value;
            }
          }
        );
        sourceNameSubscriptions.push(subscriptionId);
        
        // Get initial value
        const initialValue = window.CrComLib.getState('s', signalName);
        if (initialValue) {
          sourceNames[i - 1] = initialValue;
        }
      }
    }

    // Cleanup function
    return () => {
      if (window.CrComLib && roomNameSubscriptionId) {
        window.CrComLib.unsubscribeState('s', 'MPC3201B.RoomName', roomNameSubscriptionId);
      }
      
      sourceNameSubscriptions.forEach((subId, index) => {
        if (window.CrComLib) {
          window.CrComLib.unsubscribeState('s', `MPC3201B.Source${index + 1}Name`, subId);
        }
      });
    };
  });

  // Control functions using EXACT working patterns
  function handlePowerToggle() {
    powerSignal.pulse();
  }

  function handleVolumeUp() {
    volumeUpSignal.pulse();
  }

  function handleVolumeDown() {
    volumeDownSignal.pulse();
  }

  function handleMuteToggle() {
    muteToggleSignal.pulse();
  }

  function handleSourceSelect(sourceId: number) {
    sourceSignal.setValue(sourceId);
  }

  // Compute volume percentage for display
  $: volumePercentage = Math.round((volumeFeedbackSignal.value / 65535) * 100);
</script>

<div class="ch5-minimal-scaffold">
  <!-- Header -->
  <header class="scaffold-header">
    <h1>{roomName}</h1>
    <div class="subtitle">Minimal CH5 Scaffold</div>
  </header>

  <!-- Main Controls -->
  <main class="scaffold-main">
    <!-- Power Control -->
    <section class="control-section">
      <h2>Power</h2>
      <button class="power-btn" on:click={handlePowerToggle}>
        ⚡ System Power
      </button>
    </section>

    <!-- Volume Controls -->  
    <section class="control-section">
      <h2>Audio</h2>
      <div class="volume-display">
        <div class="volume-bar">
          <div class="volume-fill" style="width: {volumePercentage}%"></div>
        </div>
        <span class="volume-text">{volumePercentage}%</span>
      </div>
      <div class="volume-buttons">
        <button class="vol-btn" on:click={handleVolumeDown}>Vol -</button>
        <button class="mute-btn {muteFeedbackSignal.value ? 'muted' : ''}" on:click={handleMuteToggle}>
          {muteFeedbackSignal.value ? 'Unmute' : 'Mute'}
        </button>
        <button class="vol-btn" on:click={handleVolumeUp}>Vol +</button>
      </div>
    </section>

    <!-- Source Selection -->
    <section class="control-section">
      <h2>Sources</h2>
      <div class="source-current">
        Current: {sourceNames[feedbackSignal.value - 1] || 'None'}
      </div>
      <div class="source-grid">
        {#each sourceNames as sourceName, index}
          <button 
            class="source-btn {feedbackSignal.value === (index + 1) ? 'active' : ''}"
            on:click={() => handleSourceSelect(index + 1)}
          >
            {sourceName}
          </button>
        {/each}
      </div>
    </section>
  </main>

  <!-- Footer -->
  <footer class="scaffold-footer">
    <div class="status">
      Ready • Contract: MPC3201B • Connected
    </div>
  </footer>
</div>

<style>
  .ch5-minimal-scaffold {
    height: 100vh;
    display: flex;
    flex-direction: column;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    color: white;
  }

  .scaffold-header {
    background: rgba(255, 255, 255, 0.95);
    color: #333;
    padding: 20px;
    text-align: center;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }

  .scaffold-header h1 {
    margin: 0;
    font-size: 24px;
    font-weight: bold;
  }

  .subtitle {
    font-size: 14px;
    color: #666;
    margin-top: 5px;
  }

  .scaffold-main {
    flex: 1;
    padding: 20px;
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
    gap: 20px;
    overflow-y: auto;
  }

  .control-section {
    background: rgba(255, 255, 255, 0.9);
    border-radius: 12px;
    padding: 20px;
    box-shadow: 0 4px 6px rgba(0,0,0,0.1);
    color: #333;
  }

  .control-section h2 {
    margin: 0 0 15px 0;
    font-size: 18px;
    color: #2196F3;
    border-bottom: 2px solid #2196F3;
    padding-bottom: 5px;
  }

  /* Power Button */
  .power-btn {
    width: 100%;
    padding: 15px;
    font-size: 16px;
    font-weight: bold;
    border: none;
    border-radius: 8px;
    background: #4CAF50;
    color: white;
    cursor: pointer;
    transition: all 0.2s ease;
    min-height: 50px;
  }

  .power-btn:hover {
    background: #388E3C;
    transform: translateY(-2px);
  }

  /* Volume Controls */
  .volume-display {
    margin-bottom: 15px;
  }

  .volume-bar {
    width: 100%;
    height: 20px;
    background: #ddd;
    border-radius: 10px;
    overflow: hidden;
    margin-bottom: 8px;
  }

  .volume-fill {
    height: 100%;
    background: linear-gradient(90deg, #4CAF50, #8BC34A);
    transition: width 0.3s ease;
  }

  .volume-text {
    font-weight: bold;
    color: #333;
  }

  .volume-buttons {
    display: flex;
    gap: 10px;
  }

  .vol-btn, .mute-btn {
    flex: 1;
    padding: 12px;
    font-size: 16px;
    font-weight: bold;
    border: none;
    border-radius: 6px;
    cursor: pointer;
    transition: all 0.2s ease;
    min-height: 50px;
  }

  .vol-btn {
    background: #2196F3;
    color: white;
  }

  .vol-btn:hover {
    background: #1976D2;
    transform: translateY(-2px);
  }

  .mute-btn {
    background: #FF9800;
    color: white;
  }

  .mute-btn.muted {
    background: #F44336;
  }

  .mute-btn:hover {
    transform: translateY(-2px);
  }

  /* Source Controls */
  .source-current {
    margin-bottom: 15px;
    font-weight: bold;
    font-size: 16px;
    color: #2196F3;
    text-align: center;
  }

  .source-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 10px;
  }

  .source-btn {
    padding: 15px 10px;
    border: 2px solid #ddd;
    border-radius: 8px;
    background: white;
    color: #333;
    cursor: pointer;
    transition: all 0.2s ease;
    font-weight: bold;
    min-height: 60px;
    font-size: 14px;
  }

  .source-btn:hover {
    border-color: #2196F3;
    transform: translateY(-2px);
  }

  .source-btn.active {
    background: #2196F3;
    color: white;
    border-color: #1976D2;
  }

  .scaffold-footer {
    background: rgba(255, 255, 255, 0.95);
    color: #666;
    padding: 10px 20px;
    text-align: center;
    font-size: 12px;
    border-top: 1px solid #ddd;
  }

  /* Touch panel responsive design */
  @media (max-width: 1024px) {
    .scaffold-main {
      grid-template-columns: 1fr;
      padding: 15px;
    }
    
    .source-grid {
      grid-template-columns: repeat(2, 1fr);
    }
  }

  @media (min-width: 1280px) {
    .scaffold-main {
      grid-template-columns: repeat(3, 1fr);
    }
  }
</style>