<template>
  <nav class="navbar tag navbar-expand-lg navbar-dark bg-light px-2 justify-content-between shading">
    <div class="d-flex">
      <router-link :to="{ name: 'Home' }" class="btn text-dark bg-secondary selectable h-50 mobile-small" title="Home">
        Home
      </router-link>

      <button class="navbar-toggler mobile-hide" type="button" data-bs-toggle="collapse"
        data-bs-target="#navbarNavDarkDropdown" aria-controls="navbarNavDarkDropdown" aria-expanded="false"
        aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="mobile-buttons">

        <button class="btn text-dark bg-secondary selectable h-50 mobile-small" title="Create New Keep"
          aria-label="Create New Keep" data-bs-toggle="modal" data-bs-target="#createKeepModal"><small>New
            Keep</small></button>
        <button class="btn text-dark bg-secondary selectable h-50 mobile-small" title="Create New Vault"
          aria-label="Create New Vault" data-bs-toggle="modal" data-bs-target="#createVaultModal"><small>New
            Vault</small></button>
      </div>
      <div v-if="account.id" class="collapse navbar-collapse" id="navbarNavDarkDropdown">
        <ul class="navbar-nav">
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle text-dark" title="Create" aria-label="Create Menu" role="button"
              data-bs-toggle="dropdown" aria-expanded="false">
              Create
            </a>
            <ul class="dropdown-menu dropdown-menu-light">
              <li><a class="dropdown-item selectable no-select" title="Create New Keep" aria-label="Create New Keep"
                  data-bs-toggle="modal" data-bs-target="#createKeepModal">new keep</a></li>
              <li>
                <hr class="dropdown-divider">
              </li>
              <li><a class="dropdown-item selectable no-select" title="Create New Vault" aria-label="Create New Vault"
                  data-bs-toggle="modal" data-bs-target="#createVaultModal">new
                  vault</a></li>
            </ul>
          </li>
        </ul>
      </div>
    </div>

    <div v-if="account.id">

      <div class="d-flex flex-column align-items-center logo">
        <img alt="logo" src="../assets/img/Keepr-logo.png" height="45" class="" title="Keepr" />
      </div>
    </div>
    <div v-else>

      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/Keepr-logo.png" height="45" class="" title="Keepr" />
      </div>
    </div>
    <div class="me-4">
      <Login />
    </div>

  </nav>
  <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
    aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarText">
    <ul class="navbar-nav me-auto">
      <li><a class= href=""></a>
      </li>
    </ul>
  </div> -->
  <CreateKeepModal />
  <CreateVaultModal />

</template>

<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import Login from './Login.vue'
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.mobile-buttons {
  display: none;
}

.logo {
  margin-right: 8rem;
}

@media screen and (max-width: 768px) {
  nav {
    height: 64px;
    padding-left: 0;
    margin-right: 1rem
  }

  .logo {
    margin-right: 0;
  }

  .tag {
    position: fixed;
    bottom: 0;
    z-index: 10;
    width: 100%;
    height: 9%;

  }

  .mobile-hide {
    display: none;
  }

  .mobile-buttons {
    display: contents;
  }

  .mobile-small {
    font-size: small;
    padding: 1;
    margin-inline-end: .25rem;
  }
}

.shading {
  box-shadow: 0px 3px 3px rgba(174, 171, 171, 0.666);
}
</style>
