<script lang="ts">
  // This is the main viewport for control panels and content
</script>

<main class="flex-1 flex flex-col bg-[#0A1C3A] overflow-hidden">
  <!-- Content area with enhanced styling --> 
  <div class="content-area flex-1 flex overflow-hidden">
    <!-- Main content slot -->  
    <div class="content-container center-content">
      <div class="w-24 h-24 mb-6 bg-[#505A75] rounded-full flex items-center justify-center">
        <i class="fa-solid fa-display text-4xl text-white"></i>
      </div>
      <h2 class="text-white text-3xl font-medium mb-2">Touch Panel Scaffold</h2>
      <p class="text-white text-xl mb-8">Control System Template</p>
      <p class="text-white/70 text-lg">Select a source to begin</p>
    </div>
    <slot />
  </div>
</main>

<style>
  /* Apply shadow box styling from user preferences */
  .content-area {
    background-color: #0A1C3A;
    padding: 0;
    margin: 0;
  }
  
  .center-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
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
  
  /* Ensure no padding on display containers */
  :global(.display-area > div) {
    padding: 0 !important;
  }
  
  :global(.display-area > div > div > div) {
    padding: 0 !important;
  }
</style>
