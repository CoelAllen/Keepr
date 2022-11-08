<template>
  <div class="row">
    <div class="col-md-8">
      <img :src="account.picture" alt="">
      <!-- NOTE have to determine whether or not to use a profile page or v-if this shit to oblivion 
      probably have to set up profile page-->
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
