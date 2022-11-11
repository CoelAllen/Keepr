<template>




  <div class="col-5 col-md-2 vault-card rounded d-flex m-3 align-items-end">
    <div class="p-2 d-flex flex-row justify-content-between align-items-center">

      <div v-if="vault.isPrivate">
        <i class="mdi mdi-lock me-2 fs-3 lock" title="Private Vault"></i>
      </div>
      <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        <h3 class="text-light" aria-label="Go To Vault" :title="vault.name">{{ vault.name }}</h3>
      </router-link>

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
  background-size: cover;
  background-position: center;
}

h3 {
  text-shadow: 1px 1px 2px rgb(6, 6, 6);
  color: antiquewhite;
}

.lock {
  text-shadow: 1px 2px 2px rgb(206, 203, 203);

}
</style>