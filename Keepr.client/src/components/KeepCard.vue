<template>
  <div @click="getKeepById()" class="selectable rounded elevation-5 mb-4" data-bs-toggle="modal"
    data-bs-target="#keepDetail">
    <div class="card-row">
      <img :src="keep.img" class="img-fluid rounded image" :alt="keep.name" title="keep.name">
      <div class="rounded">
        <h4 class="text-light mb-1 text ps-2">{{ keep.name }}</h4>
        <div v-show="$route.name != 'Account' && $route.name != 'Vault'">
          <div v-if="account.id != keep.creatorId" class="text-2">
            <img :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name"
              class="img-fluid creator-img">
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultKeepsService } from '../services/VaultKeepService.js';
import Pop from '../utils/Pop.js';

export default {
  props: {
    keep: {
      type: Keep,
      required: true
    }
  },
  setup(props) {
    return {
      img: computed(() => `url(${props.keep?.img})`),
      account: computed(() => AppState.account),
      vaultKeep: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      async getKeepById() {
        try {
          await keepsService.getKeepById(props.keep.id)

        } catch (error) {
          Pop.error(error, "gettingKeepById")
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.creator-img {
  border-radius: 50%;
  height: 3vh;
}



.keep-card {
  position: absolute;
  bottom: 10px;
}

.delete {
  color: red;
}

.image {
  position: relative;

}

.text {
  position: absolute;
  bottom: .5rem;
}

.text-2 {
  position: absolute;
  bottom: .5rem;
  right: .5rem;
}

h4 {
  text-shadow: 1px 1px 2px rgb(6, 6, 6)
}

@media screen and (max-width: 768px) {
  .creator-img {
    display: none;
  }
}
</style>