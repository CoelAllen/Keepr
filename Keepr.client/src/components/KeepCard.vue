<template>
  <div class="row container-fluid">
    <div class="keep-card d-flex align-items-end justify-content-between rounded">
      <div v-if="account?.id == keep?.creatorId">
        <i class="mdi mdi-alpha-x-circle delete" title="Delete Keep" @click="removeKeep(keep.id)"></i>

      </div>
      <div class="p-2">
        <h3 class="text-light">{{ keep.name }}</h3>
      </div>
      <div v-if="account.id != keep.creatorId">
        <img :src="keep.creator.picture" :alt="keep.creator.name" class="img-fluid creator-img mb-2">
      </div>
    </div>
  </div>




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

      async removeKeep(id) {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this keep?')
          if (!yes) { return }
          await keepsService.removeKeep(id)
        } catch (error) {
          Pop.error(error, ('[deletingKeep]'))
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keep-card {
  background-image: v-bind(img);
  display: inline-block;
  max-height: 100%;
  width: 20vw;
  position: relative;
  ;

}

.creator-img {
  border-radius: 50%;
  height: 4vh;
}

h3 {
  text-shadow: 1px 1px 2px rgb(6, 6, 6)
}

.delete {
  color: red;
  position: absolute;
  top: -10px;
  right: -8px;

}
</style>