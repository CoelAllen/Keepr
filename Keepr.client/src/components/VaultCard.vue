<template>



  <div class="row container-fluid">

    <div class="vault-card d-flex align-items-end">
      <div class="p-2">
        <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
          <h3 class="text-light">{{ vault.name }}</h3>
        </router-link>
      </div>
    </div>
  </div>

</template>


<script>
import { computed } from '@vue/reactivity';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      img: computed(() => `url(${props.vault.img})`),
    }
  }
}
</script>


<style lang="scss" scoped>
.vault-card {
  background-image: v-bind(img);
  min-height: 20vh;
  width: 20vw;
}
</style>