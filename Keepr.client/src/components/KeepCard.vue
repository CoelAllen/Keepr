<template>
  <div @click="getKeepById()" class="selectable top-div rounded elevation-5 mb-4" data-bs-toggle="modal"
    data-bs-target="#keepDetail">
    <img :src="keep.img" class="img-fluid rounded" alt="">
    <div class="keep-card  rounded">

      <div class="d-flex align-items-center ps-2">

        <div>
          <h4 class="text-light mb-1">{{ keep.name }}</h4>
        </div>
        <div v-if="account.id != keep.creatorId">
          <img :src="keep.creator.picture" :alt="keep.creator.name" class="img-fluid creator-img spread">
        </div>
      </div>
    </div>
  </div>


  <!-- implement my get by id on my homepage here -->
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
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
  height: 4vh;
}

.keep-card {
  position: absolute;
  bottom: 10px;
}

.top-div {
  position: relative;


}

.spread {
  position: absolute;
  left: 15vw;
  bottom: 0px
}


h4 {
  text-shadow: 1px 1px 2px rgb(6, 6, 6)
}
</style>