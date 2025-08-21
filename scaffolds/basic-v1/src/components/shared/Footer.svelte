<script lang="ts">
  // Props - audio controls
  export let isMuted = false;
  export let currentVolume = 50;
  export let toggleMute = () => {};
  export let handleVolumeUp = () => {};
  export let handleVolumeDown = () => {};
  
  let isMicMuted = false;
  
  function toggleMicrophone() {
    isMicMuted = !isMicMuted;
    console.log('Microphone toggled');
  }
  
  function handlePower() {
    console.log('Power button pressed');
  }
</script>

<!-- White separator line -->
<div class="white-line"></div>

<!-- Footer with enhanced styling -->
<div class="relative w-full" style="position: relative; height: 100px; background-color: #0C234B; min-width: 1280px;">
  <!-- Footer with increased size -->
  <footer class="relative overflow-visible text-white w-full h-full" style="min-width: 1280px;">
    <!-- Full gradient overlay -->
    <div class="gradient-overlay" style="position: absolute; top: 0; left: 0; right: 0; bottom: 0; z-index: 0;"></div>
    <!-- White divider line above footer -->
    <div style="position: absolute; top: -2px; left: 0; right: 0; height: 2px; background-color: white; z-index: 1;"></div>
    <!-- Content container -->
    <div class="footer-content">
    <!-- Left side - Power button -->
    <div class="footer-left">
      <button class="power-button" on:click={handlePower} aria-label="Power">
        <i class="fas fa-power-off"></i>
      </button>
    </div>
    
    <!-- Right side - Volume controls 50px from right edge -->
    <div class="footer-right">
      <!-- Microphone button -->
      <button class="control-button {isMicMuted ? 'muted' : ''}" on:click={toggleMicrophone}>
        <i class={`fas ${isMicMuted ? 'fa-microphone-slash' : 'fa-microphone'}`}></i>
      </button>
      
      <!-- Mute button -->
      <button class="control-button {isMuted ? 'muted' : ''}" on:click={toggleMute}>
        <i class={`fas ${isMuted ? 'fa-volume-mute' : 'fa-volume-off'}`}></i>
      </button>
      
      <!-- Volume down -->
      <button class="control-button" on:click={handleVolumeDown}>
        <i class="fas fa-volume-down"></i>
      </button>
      
      <!-- Volume slider -->
      <div class="volume-slider">
        <div class="volume-track">
          <div class="volume-fill" style="width: {currentVolume}%;"></div>
        </div>
      </div>
      
      <!-- Volume percentage -->
      <div class="volume-percentage">{currentVolume}%</div>
      
      <!-- Volume up -->
      <button class="control-button" on:click={handleVolumeUp}>
        <i class="fas fa-volume-up"></i>
      </button>
    </div>
    </div>
    <!-- Subtle overlay for depth -->
    <div class="absolute inset-0 bg-gradient-to-b from-transparent to-black opacity-10"></div>
  </footer>
</div>

<style>
  /* White separator line */
  .white-line {
    height: 2px;
    background-color: white;
    width: 100%;
  }
  
  .footer-content {
    position: relative;
    z-index: 10;
    width: 100%;
    height: 100%;
    padding: 0;
  }
  
  .footer-left {
    position: absolute;
    left: 1rem;
    top: 50px;
    transform: translateY(-50%);
    display: flex;
    align-items: center;
  }
  
  .footer-right {
    position: absolute;
    right: 50px;
    top: 50px;
    transform: translateY(-50%);
    display: flex;
    align-items: center;
    gap: 0.25rem;
  }
  
  .power-button {
    width: 5rem;
    height: 5rem;
    background: #12406B;
    border: none;
    border-radius: 50%;
    color: white;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: background-color 0.2s;
    box-shadow: 0 4px 8px rgba(0,0,0,0.3);
  }
  
  .power-button:hover {
    background: #235685;
  }
  
  .power-button i {
    font-size: 2.5rem;
  }
  
  .control-button {
    width: 4.2rem;
    height: 4.2rem;
    background: #12406B;
    border: none;
    border-radius: 50%;
    color: white;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: background-color 0.2s;
    box-shadow: 0 2px 4px rgba(0,0,0,0.2);
  }
  
  .control-button:hover {
    background: #235685;
  }
  
  .control-button.muted {
    background: #AB0520;
    border: 4px solid #0C234B;
  }
  
  .control-button i {
    font-size: 1.75rem;
  }
  
  .volume-slider {
    width: 8rem;
    height: 1rem;
    position: relative;
  }
  
  .volume-track {
    width: 100%;
    height: 100%;
    background: #12406B;
    border-radius: 0.5rem;
    position: relative;
    box-shadow: inset 0 2px 4px rgba(0,0,0,0.3);
  }
  
  .volume-fill {
    height: calc(100% - 2px);
    background: white;
    border-radius: 0.5rem;
    margin: 1px;
    transition: width 0.2s;
  }
  
  .volume-percentage {
    color: white;
    font-family: monospace;
    font-size: 0.875rem;
    text-align: center;
    min-width: 2.5rem;
  }
</style>
