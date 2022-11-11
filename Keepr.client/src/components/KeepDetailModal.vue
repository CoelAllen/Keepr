<template>
  <div class="modal fade" id="keepDetail" tabindex="-1" aria-labelledby="keepDetailModal" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content" v-if="keep">

        <div class="row rounded">

          <img :src="keep.img" :alt="keep.name" class="col-md-6" data-bs-dismiss="modal">
          <!-- DELETE -->

          <div class="col-md-6 d-flex flex-column justify-content-between">
            <div v-if="account?.id == keep?.creatorId" class="text-end p-1">
              <i class="mdi mdi-alpha-x-circle delete selectable" data-bs-dismiss="modal" title="Delete Keep"
                aria-label="Delete Keep" @click="removeKeep(keep.id)"></i>
            </div>

            <!-- KEEPS KEPT COUNTER -->
            <div class="d-flex align-items-center justify-content-center m-2 p-3">
              <p class="pe-2" title="Times Viewed"><i class="mdi mdi-eye-outline" :title="keep.views"></i> {{
                  keep.views
              }}</p>
              <p class="d-flex align-items-center pe-2" title="Times Kept">
              <div class="border border-dark px-1 me-1" :title="keep.kept">k</div>{{ keep.kept }}</p>
            </div>
            <!-- NAME AND DESC -->
            <div class="text-center">
              <h2 :title="keep.name">{{ keep.name }}</h2>
            </div>
            <div class="p-5">
              <p :aria-label="keep.description">{{ keep.description }}</p>
            </div>
            <!-- USER INPUTS -->
            <div class="d-flex me-2 mb-2">

              <!-- TODO working on the v-if to remove from vault -->
              <div v-if="$route.name == 'Vault' && vault.creatorId == account.id" class="remove">
                <i @click="removeVaultKeep()" class="mdi mdi-cancel selectable" title="Remove Keep from Vault"
                  aria-label="Remove Keep from Vault"></i> Remove
              </div>



              <div class="d-flex p-2" v-else-if="account.id">
                <div class="me-1">
                  <select class="form-select" v-model="editable.vaultId" aria-label="Select Vault">
                    <option selected>Vaults</option>
                    <option v-for="m in myVaults" :key="m.id" :value="m.id" :title="m.name" :aria-label="m.name">
                      {{ m.name }}
                    </option>
                  </select>
                </div>
                <button class="btn btn-primary" @click="saveKeepToVault()" data-bs-dismiss="modal"
                  title="Save Keep to Vault" aria-label="Save Keep to Vault">Save</button>
              </div>
              <!-- TODO see above note -->

              <!-- routerlink -->
              <div class="d-flex align-items-center">
                <!-- <h1>keep.creatorId : {{ keep.creatorId }}</h1> -->
                <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                  <img :src="keep.creator?.picture" class="creator-img" alt="" :title="keep.creator?.name"
                    aria-label="Go to Profile" data-bs-dismiss="modal">
                </router-link>
                <div class="ms-1" :title="keep.creator?.name">{{ keep.creator?.name }}</div>
              </div>
            </div>

          </div>
        </div>


      </div>
      <div v-else>
        loading....
      </div>
    </div>
  </div>

</template>


<script>
import { ref } from 'vue';

import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js';
import { vaultKeepsService } from '../services/VaultKeepService.js';
import Pop from '../utils/Pop.js';
import { Keep } from '../models/Keep.js';

export default {
  props: {
    keep: {
      type: Keep,
      required: true
    }
  },
  setup() {
    const editable = ref({})

    return {
      editable,
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      vault: computed(() => AppState.activeVault),
      vaultKeep: computed(() => AppState.vaultKeeps.filter((v) => v.id == AppState.activeKeep.id)),
      async removeKeep(id) {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this keep?')
          if (!yes) { return }
          await keepsService.removeKeep(id)
        } catch (error) {
          Pop.error(error, ('[deletingKeep]'))
        }
      },
      async saveKeepToVault() {
        try {
          var keep = AppState.activeKeep
          console.log(editable.value, AppState.activeKeep, "HEY");
          await vaultKeepsService.createVaultKeep(editable.value, keep.id)
          editable.value = {}
        } catch (error) {
          Pop.error(error, ('[creatingVaultKeep]'))
        }
      },
      async removeVaultKeep() {
        try {
          console.log(AppState.vaultKeeps.filter((v) => v.id == AppState.activeKeep.id));
          let vaultKeep = AppState.vaultKeeps.filter((v) => v.id == AppState.activeKeep.id);
          console.log(AppState.activeKeep);
          await vaultKeepsService.deleteVaultKeep(vaultKeep[0].vaultKeepId)
        } catch (error) {
          Pop.error(error, ("[deletingVaultKeep]"))
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.creator-img {
  border-radius: 50%;
  height: 4vh;
}

.delete {
  color: red;
}

.remove {
  border-bottom: 2px solid gray;
}

// .modal-body {
//   flex-direction
// }
</style>