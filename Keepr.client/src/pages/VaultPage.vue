<template>
  <div class="row container-fluid justify-content-center">
    <div class="col-md-12 mt-3 vault-image">
      <div class="vault-title">
        <div class="growBoy"></div>
        <h1>{{ vault.name }}</h1>
        <h5>by {{ vault.creator?.name }}</h5>
      </div>
    </div>





    <div class="row justify-content-center">
      <div class="col-md-8 d-flex flex-wrap">

        <div class="col-md-3 py-3 selectable" data-bs-toggle="modal" data-bs-target="#keepDetail" v-for="k in keeps">
          <KeepCard :key="k.id" :keep="k" @click="makeKeepActive(k)" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const route = useRoute();
    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.vaultId);
      }
      catch (error) {
        Pop.error("[GetKeepsByVault]");
      }
    }
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.vaultId);
      }
      catch (error) {
        Pop.error("[GetVaultById]");
      }
    }
    onMounted(() => {
      getVaultById();
      getKeepsByVaultId();
    });
    return {
      keeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      img: computed(() => `url(${AppState.activeVault?.img})`)
    };
  },
  components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.vault-image {
  border-radius: 5px;
  background-image: v-bind(img);
  background-size: fill;
  background-position: center;
  height: 33vh;
  width: 40vw
}

.vault-title {
  display: flex;
  flex-direction: column;
  justify-content: baseline;
  align-items: center;
  color: aliceblue;
  text-shadow: 1px 1px 2px rgb(6, 6, 6);
  flex-grow: 10 10;
}

.growBoy {
  flex-direction: column;
  height: 18vh;
}
</style>