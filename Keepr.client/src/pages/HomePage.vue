<template>
  <div class="container-fluid">
    <div class="row m-5 justify-content-center">
      <div class="col-md-10 masonry-with-columns p-3 mt-2">
        <KeepCard :key="k.id" :keep="k" v-for="k in keeps" />
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

      // async getKeepById() {
      //   try {
      //     await keepsService.getKeepById()
      //   } catch (error) {
      //     Pop.error(error, "[gettingKeepById]")
      //   }
      // }
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
  columns: 4;
  column-gap: 1rem;


  img.photo {
    width: 20vw;
    margin-top: 1.5rem
  }
}
</style>
