<script lang="ts">
  import { createEventDispatcher } from 'svelte';
  export let className = '';
  export let style = '';
  
  const dispatch = createEventDispatcher();
</script>

<!--
  ShadowboxContainer: Reusable styled container for control panels and UI sections.
  Usage: <ShadowboxContainer ariaLabel="Description">...</ShadowboxContainer>
  Accessibility: ARIA role set to 'region' for grouping, tabindex removed.
-->
<div
  class={`shadowbox-container ${className}`}
  style={style}

>
  <slot />
</div>

<style>
.shadowbox-container {
  background: #12406B;
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 0.75rem;
  box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.3),
    0 4px 6px -2px rgba(0, 0, 0, 0.2),
    inset 0 2px 4px rgba(255, 255, 255, 0.1),
    0 0 0 1px rgba(255, 255, 255, 0.1);
  padding: 2rem;
  width: 100%;
  height: 100%;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: stretch;
  outline: none;
}
</style>