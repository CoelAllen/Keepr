<template>
  <div class="container-fluid">
    <div class="row m-5">
      <div class="col-md-3  masonry-with-columns p-3 selectable" data-bs-toggle="modal" data-bs-target="#keepDetail"
        v-for="k in keeps">
        <KeepCard :key="k.id" :keep="k" @click="makeKeepActive(k)" />
      </div>
    </div>
  </div>

</template>

<script>
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import { onMounted } from 'vue';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
  setup() {
    async function getAllKeeps() {
      try {

        // NOTE data dump, gotta make service
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
        Pop.error(error.message
        )
      }
    }
    onMounted(() => {
      getAllKeeps()
    })
    return {
      keeps: computed(() => AppState.keeps),
      async makeKeepActive(keep) {
        AppState.activeKeep = keep;
        console.log(keep);
      }
    }
  }
}
</script>

<style scoped lang="scss">
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;


  // div {
  //   width: 150px;
  //   color: white;
  //   margin: 0 1rem 1rem 0;
  //   display: inline-block;
  //   width: 100%;

  //   font-family: system-ui;
  //   font-weight: 900;
  //   font-size: 2rem;
  // }

}
</style>
