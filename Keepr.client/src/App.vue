<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepDetailModal />
  <footer class="mt-2"></footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import CreateKeepModal from './components/CreateKeepModal.vue'
import KeepDetailModal from './components/KeepDetailModal.vue'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepDetailModal, CreateKeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

main {
  overflow-x: hidden;
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
