<script lang="ts">
  import Header from './components/shared/Header.svelte';
  import Footer from './components/shared/Footer.svelte';
  
  // Export variables and functions for child components
  export let roomName = "Room Name";
  export let isMuted = false;
  export let currentVolume = 50;
  
  // Source management
  let currentSource = 0;
  const sources = [
    { id: 1, label: 'PC', icon: 'fa-solid fa-desktop' },
    { id: 2, label: 'Laptop', icon: 'fa-solid fa-laptop' },
    { id: 3, label: 'Airmedia', icon: 'fa-solid fa-wifi' },
    { id: 4, label: 'Doc Cam', icon: 'fa-solid fa-video' },
    { id: 5, label: 'Lectern HDMI', icon: 'fa-solid fa-plug' },
    { id: 6, label: 'Floor Plate', icon: 'fa-solid fa-square' },
  ];
  
  function selectSource(sourceId) {
    currentSource = sourceId;
  }
  
  function toggleMute() {
    isMuted = !isMuted;
  }
  
  function handleVolumeUp() {
    currentVolume = Math.min(100, currentVolume + 5);
  }
  
  function handleVolumeDown() {
    currentVolume = Math.max(0, currentVolume - 5);
  }
</script>

<div id="app-container" class="fixed inset-0 w-screen h-screen overflow-hidden bg-[#0C234B] flex items-center justify-center">
  <main class="relative bg-[#0a1a36] overflow-hidden" style="width: 1280px; height: 800px; transform-origin: center center;">
  
  <Header {roomName} />
  
  <!-- Main content area -->
  <div class="content-area">
  <!-- Source selection buttons -->
  <div class="source-list">
    {#each sources as source, idx (source.id)}
      <button 
        class="source-button {currentSource === source.id ? 'active' : ''}"
        on:click={() => selectSource(source.id)}
        aria-label={source.label}
        tabindex={idx}
        data-source-id={source.id}
      >
        <i class="{source.icon}"></i>
        <span>{source.label}</span>
      </button>
    {/each}
  </div>
  
  <!-- Main display area -->
  <div class="display-area">
    {#if currentSource === 0}
      <div class="content-container center-content">
        <div class="w-24 h-24 mb-6 bg-[#505A75] rounded-full flex items-center justify-center">
          <i class="fa-solid fa-display text-4xl text-white"></i>
        </div>
        <h2 class="text-white text-3xl font-medium mb-2">Touch Panel Scaffold</h2>
        <p class="text-white text-xl mb-8">Control System Template</p>
        <p class="text-white/70 text-lg">Select a source to begin</p>
      </div>
    {:else}
      <div class="source-content">
        <h2 class="text-white text-2xl mb-4">Source: {sources.find(s => s.id === currentSource)?.label}</h2>
        <p class="text-white/70">Source controls would appear here</p>
      </div>
    {/if}
  </div>
</div>

    <Footer {isMuted} {currentVolume} {toggleMute} {handleVolumeUp} {handleVolumeDown} />
  </main>
</div>

<style>
  /* Reset and base styles */
  :global(html), :global(body) {
    margin: 0;
    padding: 0;
    width: 100%;
    height: 100%;
    overflow: hidden;
    font-family: 'Segoe UI', 'SegoeUI', Arial, sans-serif;
    touch-action: manipulation;
    -webkit-tap-highlight-color: transparent;
    user-select: none;
    border: none !important;
  }
  
  :global(#app) {
    width: 100%;
    height: 100%;
    margin: 0;
    padding: 0;
    border: none !important;
    overflow: hidden;
  }
  
  /* Scale for TS-1070 (1920x1200) */
  @media (min-width: 1600px) {
    #app-container {
      width: 100%;
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
    }
    
    #app-container main {
      transform: scale(1.5);
      transform-origin: center center;
      width: 1280px;
      height: 800px;
      position: relative;
    }
  }
  
  /* Ensure main takes full dimensions */
  #app-container {
    position: fixed;
    inset: 0;
    width: 100vw;
    height: 100vh;
    overflow: visible;
    background: #0C234B;
    display: flex;
    align-items: center;
    justify-content: center;
  }
  
  /* Ensure touch targets are properly sized */
  button, [role="button"] {
    min-height: 48px;
    min-width: 48px;
  }
  
  /* Optimize text rendering */
  body {
    text-rendering: optimizeLegibility;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
  }
  
  /* Main container */
  main {
    display: flex;
    flex-direction: column;
    background-color: #0a1a36;
    color: white;
    overflow: visible;
    padding: 0;
    margin: 0;
    width: 1280px;
    height: 800px;
    position: relative;
  }
  
  /* Horizontal flex layout for content area */
  .content-area {
    display: flex;
    flex-direction: row;
    width: 100%;
    height: calc(100% - 164px); /* minus header (64px) and footer (100px) */
    min-height: 0;
    min-width: 0;
    background: none;
    overflow: hidden;
  }
  
  .source-list {
    flex: 0 0 210px;
    min-width: 210px;
    max-width: 260px;
    height: 100%;
    background: #0A1C3A;
    border-right: 1px solid rgba(255,255,255,0.1);
    box-shadow: 2px 0 8px rgba(0,0,0,0.10);
    display: flex;
    flex-direction: column;
    align-items: stretch;
    padding: 2.5% 0;
    z-index: 1;
  }
  
  .display-area {
    flex: 1 1 0%;
    min-width: 0;
    min-height: 0;
    display: flex;
    align-items: stretch;
    justify-content: center;
    background: #0A1C3A;
    padding: 0 5%;
    position: relative;
    z-index: 0;
    overflow: visible;
  }
  
  .source-button {
    display: flex;
    align-items: center;
    width: 100%;
    padding: 20px 20px;
    min-height: 75px;
    background: none;
    border: none;
    color: white;
    cursor: pointer;
    transition: none !important;
    outline: none; /* Remove focus outline */
    margin: 0 0rem;
    letter-spacing: 0.06em;
    line-height: 1.1;
  }
  
  .source-button:hover {
    background-color: rgba(255, 255, 255, 0.1);
  }
  
  .source-button.active {
    background-color: rgba(171, 5, 32, 0.5); /* UA red with transparency */
    border-left: 4px solid #AB0520;
  }
  
  .source-button i {
    width: 32px;
    margin-right: 12px;
    font-size: 28px; /* Increased icon size proportionally */
  }
  
  .content-container {
    font-size: 2rem;
    text-align: center;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    margin-top: 1rem;
    color: white;
  }
  
  .center-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
  }
  
  .source-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    text-align: center;
  }
  
  /* Shadow box styling as per user preferences */
  :global(.shadow-box) {
    background-color: #0A1C3A;
    box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.3), 
                0 4px 6px -2px rgba(0, 0, 0, 0.2), 
                inset 0 2px 4px rgba(255, 255, 255, 0.1), 
                0 0 0 1px rgba(255, 255, 255, 0.1);
    border: 1px solid rgba(255, 255, 255, 0.1);
    border-radius: 0.75rem;
  }
</style>
