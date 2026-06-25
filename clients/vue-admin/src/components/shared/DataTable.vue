<template>
  <div class="data-table">
    <table>
      <thead>
        <tr>
          <th v-for="column in columns" :key="column.key">
            {{ column.label }}
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="row in data" :key="row.id">
          <td v-for="column in columns" :key="column.key">
            {{ row[column.key] }}
          </td>
        </tr>
      </tbody>
    </table>
    
    <div v-if="data.length === 0" class="empty-state">
      <slot name="empty">
        <p>No data available</p>
      </slot>
    </div>
    
    <div v-if="showPagination" class="pagination">
      <button 
        :disabled="currentPage === 1"
        @click="$emit('page-change', currentPage - 1)"
      >
        Previous
      </button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button 
        :disabled="currentPage === totalPages"
        @click="$emit('page-change', currentPage + 1)"
      >
        Next
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
interface Column {
  key: string;
  label: string;
  sortable?: boolean;
}

interface Props {
  columns: Column[];
  data: any[];
  currentPage?: number;
  totalPages?: number;
  showPagination?: boolean;
}

withDefaults(defineProps<Props>(), {
  currentPage: 1,
  totalPages: 1,
  showPagination: false
});

defineEmits(['page-change', 'sort']);
</script>

<style scoped>
.data-table {
  width: 100%;
}

.data-table table {
  width: 100%;
  border-collapse: collapse;
}

.data-table th,
.data-table td {
  padding: 12px;
  text-align: left;
  border-bottom: 1px solid #e5e7eb;
}

.data-table th {
  background-color: #f9fafb;
  font-weight: 600;
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  margin-top: 1rem;
}

.empty-state {
  text-align: center;
  padding: 2rem;
  color: #6b7280;
}
</style>
