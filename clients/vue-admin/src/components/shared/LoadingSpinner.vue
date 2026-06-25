<template>
  <div class="loading-spinner" :class="{ 'full-page': fullPage }">
    <div class="spinner"></div>
    <span v-if="text" class="text">{{ text }}</span>
  </div>
</template>

<script setup lang="ts">
interface Props {
  text?: string;
  fullPage?: boolean;
}

withDefaults(defineProps<Props>(), {
  text: '',
  fullPage: false
});
</script>

<style scoped>
.loading-spinner {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 2rem;
}

.loading-spinner.full-page {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(255, 255, 255, 0.9);
  z-index: 9999;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 3px solid #e5e7eb;
  border-top-color: #10b981;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}

.text {
  margin-top: 1rem;
  color: #6b7280;
}
</style>
