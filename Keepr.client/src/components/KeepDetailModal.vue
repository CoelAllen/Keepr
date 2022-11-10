<template>
  <div class="modal fade" id="keepDetail" tabindex="-1" aria-labelledby="keepDetailModal" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content" v-if="keep">

        <div class="row rounded">

          <img :src="keep.img" alt="" class="col-md-6 img-fluid">
          <div class="col-md-6">
            <div>
              <div v-if="account?.id == keep?.creatorId" class="text-end p-1">
                <i class="mdi mdi-alpha-x-circle delete" title="Delete Keep" @click="removeKeep(keep.id)"></i>
              </div>

              <div class="d-flex align-items-center justify-content-center m-2 p-3">
                <p class="pe-2"><i class="mdi mdi-eye-outline"></i> {{ keep.views }}</p>
                <p class="d-flex align-items-center pe-2">
                <div class="border border-dark px-1 me-1">k</div>{{ keep.kept }}</p>
              </div>

              <div class="text-center">
                <h2>{{ keep.name }}</h2>
              </div>
              <div class="p-5">
                <p>{{ keep.description }}</p>
              </div>
              <div class="d-flex justify-content-between me-2 mb-2">

                <!-- TODO working on the v-if to remove from vault -->
                <div class="d-flex p-2" v-if="keep.id == vaultKeep.id">
                  <div class="me-1">
                    <select class="form-select" v-model="editable.vaultId" aria-label="Default select example">
                      <option :value='null' selected>Vaults</option>
                      <option v-for="m in myVaults" :key="m.id" :value="m.id">
                        {{ m.name }}
                      </option>
                    </select>
                  </div>
                  <button class="btn btn-primary" @click="saveKeepToVault()">Save</button>
                </div>
                <!-- TODO see above note -->
                <div v-else class="remove">
                  <i @click="removeVaultKeep(vaultKeep.id)" class="mdi mdi-cancel selectable"></i> Remove
                </div>

                <!-- routerlink -->
                <div class="d-flex align-items-center">
                  <!-- <h1>keep.creatorId : {{ keep.creatorId }}</h1> -->
                  <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                    <img :src="keep.creator?.picture" class="creator-img" alt="" data-bs-dismiss="modal">
                  </router-link>
                  <div class="ms-1">{{ keep.creator?.name }}</div>
                </div>
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

export default {

  setup() {
    const editable = ref({})

    return {
      editable,
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      vaultKeep: computed(() => AppState.vaultKeeps.filter((v) => v.keepId == AppState.activeKeep.id)),
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
        } catch (error) {
          Pop.error(error, ('[creatingVaultKeep]'))
        }
      },
      async removeVaultKeep(id) {
        try {
          const vaultKeep = AppState.vaultKeeps.filter((v) => v.keepId == AppState.activeKeep.id)
          console.log(vaultKeep.id);
          await vaultKeepsService.deleteVaultKeep(id)
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
</style>