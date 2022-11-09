<template>
  <div class="row">
    <div class="mt-3 col-md-12 d-flex justify-content-center">
      <img class="profile-background" src="https://thiscatdoesnotexist.com" alt="">
      <img class="profile-image " :src="profile.picture" alt="">
    </div>
  </div>
  <div class="row">
    <div class="col-md-12 mt-5 text-center">
      <h3>{{ profile.name }}</h3>
      <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
    </div>
  </div>
  <div class="row justify-content-center">
    <div class="col-md-8 ms-3" v-if="vaults.length > 0">
      <h1>Vaults</h1>
    </div>
  </div>
  <div class="row justify-content-center">
    <div class="col-md-8 d-flex flex-wrap">

      <div class="col-md-3 py-3 selectable">

        <VaultCard :key="v.id" :vault="v" v-for="v in vaults" />

      </div>
    </div>
  </div>



  <div class="row justify-content-center">
    <div class="col-md-8 ms-3" v-if="keeps.length > 0">
      <h1>Keeps</h1>
    </div>
  </div>
  <div class="row justify-content-center">
    <div class="col-md-8 masonry-with-columns p-3 mt-2">
      <KeepCard :key="k.id" :keep="k" v-for="k in keeps" />
    </div>



  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import VaultCard from '../components/VaultCard.vue';
import { accountService } from '../services/AccountService.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const route = useRoute();
    async function getProfileById() {
      try {
        await accountService.getProfile(route.params.profileId);
      }
      catch (error) {
        Pop.error("[GetProfileById]");
      }
    }
    async function getKeepsById() {
      try {
        await keepsService.getProfileKeeps(route.params.profileId);
      }
      catch (error) {
        Pop.error("[getKeepsById]");
      }
    }
    async function getVaultsById() {
      try {
        await vaultsService.getProfileVaults(route.params.profileId);
        console.log('getting vaults');
      }
      catch (error) {
        Pop.error("[getVaultsById]");
      }
    }
    onMounted(() => {
      getProfileById();
      getKeepsById();
      getVaultsById();
    });
    return {
      profile: computed(() => AppState.activeProfile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults.filter((v) => v.isPrivate == false)),

    };
  },
  components: { VaultCard }
}
</script>


<style lang="scss" scoped>
.profile-image {
  border-radius: 50%;
  height: 10vh;
  width: 7vw;
  position: absolute;
  top: 33.5rem;
  border-color: aliceblue;
  border-width: 2px;
  border-style: solid;
  box-shadow: 0px 3px 1px 2px rgba(128, 128, 128, 0.584);
}

.profile-background {
  position: relative;
}

.masonry-with-columns {
  columns: 4;
  column-gap: 1rem;


  img.photo {
    width: 20vw;
    margin-top: 1.5rem
  }
}
</style>