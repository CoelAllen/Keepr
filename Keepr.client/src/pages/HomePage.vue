<template>
  <div class="container-fluid">
    <div class="row  justify-content-center">
      <div class=" tag col-12 col-md-10 masonry-with-columns  mt-4">
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

@media screen and (max-width: 768px) {
  .masonry-with-columns {
    columns: 2;
    margin-left: 2rem;
    margin-right: 2rem;

  }
}
</style>
