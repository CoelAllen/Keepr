<template>
  <div class="row container-fluid">

    <div class="vault-card d-flex align-items-end">
      <div class="p-2">
        <h3 class="text-light">{{ vault.name }}</h3>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
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