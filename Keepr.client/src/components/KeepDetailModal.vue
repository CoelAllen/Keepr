<template>
  <div class="modal fade" id="keepDetail" tabindex="-1" aria-labelledby="keepDetailModal" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">

        <div class="row rounded">

          <img :src="keep.img" alt="" class="col-md-6">
          <div class="col-md-6">
            <div class="">

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
              <div class="d-flex align-items-center justify-content-between p-2">
                <div v-if="account.id == keep.creatorId">
                  <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown"
                      aria-expanded="false">
                      Vault
                    </button>
                    <ul class="dropdown-menu">
                      <li><a class="dropdown-item" href="#">Action</a></li>
                    </ul>
                  </div>
                  <button class="btn btn-primary">Save</button>
                </div>
              </div>
              <div class="d-flex justify-content-end me-2">

                <!-- routerlink -->
                <div class="d-flex align-items-center">
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
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {

  setup() {

    return {
      keep: computed(() => AppState.activeKeep),
      account: computed(() => AppState.account)
    }
  }
}
</script>


<style lang="scss" scoped>
.creator-img {
  border-radius: 50%;
  height: 4vh;
}
</style>