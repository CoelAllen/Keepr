<template>
  {{ myVault.name }}
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { accountService } from '../services/AccountService.js';

export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      myVault: computed(() => AppState.vaults.find(v => v.creatorId == accountService.id))
    }
  }
}
</script>


<style lang="scss" scoped>

</style>