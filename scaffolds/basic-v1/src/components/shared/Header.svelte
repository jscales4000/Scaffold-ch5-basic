<script lang="ts">
  import { onMount } from 'svelte';
  
  // Props
  export let roomName = "Room Name";
  
  // Time display
  let currentTime = '';
  
  function updateTime() {
    const now = new Date();
    const hours = now.getHours();
    const minutes = now.getMinutes().toString().padStart(2, '0');
    const ampm = hours >= 12 ? 'PM' : 'AM';
    const displayHours = hours % 12 || 12;
    currentTime = `${displayHours}:${minutes} ${ampm}`;
  }
  
  // Update time every minute
  let timeInterval;
  
  onMount(() => {
    updateTime(); // Initial call
    timeInterval = setInterval(updateTime, 60000);
    
    return () => {
      if (timeInterval) clearInterval(timeInterval);
    };
  });
  
</script>

<header class="relative px-6 bg-[#0C234B]" style="height: 64px; min-height: 64px; min-width: 1280px; width: 100%;">
  <!-- Header content positioned 1/2 of header height from center -->
  <div class="header-content">
    <div class="header-left">
      <div class="h-10 w-10 bg-[#505A75] rounded-full flex items-center justify-center">
        <i class="fa-solid fa-building text-white"></i>
      </div>
      <h1 class="room-name-header">{roomName}</h1>
    </div>
    <div class="header-right">
      <span class="time-display">{currentTime || '12:19 PM'}</span>
      <button class="header-button">
        <i class="fas fa-question-circle"></i>
        <span>Help</span>
      </button>
      <button class="header-button">
        <i class="fas fa-cog"></i>
        <span>Settings</span>
      </button>
    </div>
  </div>
</header>

<style>
  header {
    border-bottom: 1px solid rgba(255, 255, 255, 0.1);
  }
  
  .header-content {
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    height: 100%;
  }
  
  .header-left {
    display: flex;
    align-items: center;
    gap: 1rem;
  }
  
  .header-right {
    display: flex;
    align-items: center;
    gap: 1.5rem;
  }
  
  .room-name-header {
    font-size: 1.8rem;
    font-weight: 200;
    color: #fff;
    letter-spacing: 0.04em;
    line-height: 1.1;
    margin: 0;
  }
  
  .time-display {
    color: white;
    font-size: 1.25rem;
    font-weight: 500;
    white-space: nowrap;
  }

  .header-button {
    background: none;
    border: none;
    color: #FFFFFF;
    cursor: pointer;
    padding: 8px 12px;
    border-radius: 4px;
    transition: color 0.2s, transform 0.2s;
    display: flex;
    align-items: center;
    gap: 6px;
    font-size: 16px;
    font-weight: 500;
  }

  .header-button i {
    font-size: 1.25rem;
  }

  .header-button:hover,
  .header-button:focus {
    color: #AB0520;
    transform: scale(1.1);
  }
</style>
