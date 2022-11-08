<template>
  <!-- <li><a class="dropdown-item" href="#">{{}}</a></li> -->
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
  setup() {
    return {
      myVault: computed(() => AppState.vaults.find(v => v.creatorId == ac))
    }
  }
}
</script>


<style lang="scss" scoped>

</style>