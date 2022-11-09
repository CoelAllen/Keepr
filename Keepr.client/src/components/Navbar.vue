<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-light px-3 justify-content-between shading">
    <div class="d-flex">
      <router-link :to="{ name: 'Home' }" class="btn text-dark bg-secondary selectable h-50">
        Home
      </router-link>

      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavDarkDropdown"
        aria-controls="navbarNavDarkDropdown" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNavDarkDropdown">
        <ul class="navbar-nav">
          <li class="nav-item dropdown">
            <a class="nav-link dropdown-toggle text-dark" href="#" role="button" data-bs-toggle="dropdown"
              aria-expanded="false">
              Create
            </a>
            <ul class="dropdown-menu dropdown-menu-light">
              <li><a class="dropdown-item selectable no-select" data-bs-toggle="modal"
                  data-bs-target="#createKeepModal">new keep</a></li>
              <li>
                <hr class="dropdown-divider">
              </li>
              <li><a class="dropdown-item selectable no-select" data-bs-toggle="modal"
                  data-bs-target="#createVaultModal">new
                  vault</a></li>
            </ul>
          </li>
        </ul>
      </div>
    </div>


    <div class="d-flex flex-column align-items-center me-5">
      <img alt="logo" src="../assets/img/Keepr-logo.png" height="45" />
    </div>

    <Login />
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
import Login from './Login.vue'
export default {
  setup() {
    return {}
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

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

.shading {
  box-shadow: 0px 3px 3px rgba(174, 171, 171, 0.666);
}
</style>
