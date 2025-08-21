<script lang="ts">
  // Source definitions - typically would come from configuration
  const sources = [
    { id: 'display', label: 'Display', icon: 'fa-solid fa-display' },
    { id: 'laptop', label: 'Laptop', icon: 'fa-solid fa-laptop' },
    { id: 'camera', label: 'Camera', icon: 'fa-solid fa-video' },
    { id: 'media', label: 'Media Player', icon: 'fa-solid fa-play' }
  ];
  
  // Track selected source
  let selectedSourceId = '';
  
  function selectSource(id) {
    selectedSourceId = id;
    // In a real app, would emit an event or update a store
    console.log(`Selected source: ${id}`);
  }
</script>

<aside class="w-[200px] bg-[#0A1C3A] shadow-box">
  <div class="source-list p-4 flex flex-col space-y-2">
    <h2 class="text-white text-lg font-medium mb-2">Sources</h2>
    
    {#each sources as source}
      <button 
        class="source-button flex items-center space-x-2 py-3 px-4 rounded-md text-white
              {selectedSourceId === source.id ? 'bg-[#AB0520]' : 'bg-[#0C234B] hover:bg-[#0C234B]/80'}"
        on:click={() => selectSource(source.id)}
      >
        <i class="{source.icon} w-6"></i>
        <span>{source.label}</span>
      </button>
    {/each}
  </div>
</aside>
