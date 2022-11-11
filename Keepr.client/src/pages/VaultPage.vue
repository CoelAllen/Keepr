<template>
  <div class="row container-fluid justify-content-center">
    <div class="col-md-12 mt-3 vault-image">
      <div class="vault-title">
        <div class="growBoy"></div>
        <h1 :title="vault.name">{{ vault.name }}</h1>
        <h5 :title="vault.creator?.name">by {{ vault.creator?.name }}</h5>
      </div>
    </div>
    <div class="col-md-12 d-flex justify-content-center mt-4">
      <div class="keep px-3 py-1" :title="keeps.length">
        {{ keeps.length }} Keeps
      </div>
      <div v-if="vault.creatorId == account.id">
        <div class="dropdown ms-2">
          <div class="dropdown-toggle" title="Edit or Delete Vault" type="button" data-bs-toggle="dropdown">
            ...
          </div>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" data-bs-toggle="modal" data-bs-target="#editVault">Edit</a></li>
            <li><a class="dropdown-item" @click="deleteVault(vault.id)">Delete</a></li>
          </ul>
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-8 masonry-with-columns p-3 mt-2">
        <KeepCard :key="k.id" :keep="k" v-for="k in keeps" />

      </div>
    </div>
  </div>
  <!-- NOTE modal for edit -->
  <form @submit.prevent="editVault(vault.id)" class="modal fade" id="editVault" tabindex="-1"
    aria-labelledby="editVaultLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="d-flex justify-content-between p-3 modal-head">
          <h1 class="modal-title fs-5" id="editVaultLabel">Edit your vault</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-head">
          <div>
            <div class="form-floating m-3">
              <input type="text" v-model="editable.name" class="modal-body form-control pe-3" id="titleInput"
                placeholder="Title...">
              <label for="titleInput" class="modal-input">Title</label>
            </div>
            <div class="form-floating m-3">
              <input type="url" v-model="editable.img" class="modal-body form-control pe-3" id="imageInput"
                placeholder="Image URL...">
              <label for="imageInput" class="modal-input">Image URL...</label>
            </div>
            <div class="form-floating m-3">
              <input type="text" v-model="editable.description" class="modal-body form-control pe-3"
                id="descriptionInput" placeholder="Description...">
              <label for="descriptionInput" class="modal-input">Description...</label>
            </div>
            <div class="me-2 mb-2 text-end">
              <input v-model="editable.isPrivate" class="form-check-input me-1" type="checkbox" value="" id="check">
              <label class="form-check-label check-outline" for="check">
                Make Vault Private?
              </label>
            </div>
          </div>
        </div>
        <div class="d-flex modal-head justify-content-end">
          <button type="button" class="btn submit-btn m-1 b-submit" data-bs-dismiss="modal">Close</button>
          <button type="submit" class="btn submit-btn m-1 b-submit">Save changes</button>
        </div>
      </div>
    </div>
  </form>
</template>


<script>
import { computed } from '@vue/reactivity';
import { Modal } from 'bootstrap';
import { ref } from 'vue';
import { onMounted, popScopeId } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import KeepCard from '../components/KeepCard.vue';
import { router } from '../router.js';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    const route = useRoute();
    async function getKeepsByVaultId() {
      try {
        await vaultsService.getKeepsByVaultId(route.params.vaultId);
      }
      catch (error) {
        Pop.error("[GetKeepsByVault]");
        router.push({ name: 'Home' })
      }
    }
    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.vaultId);
      }
      catch (error) {
        Pop.error("[GetVaultById]");
        router.push({ name: 'Home' })
      }
    }
    onMounted(() => {
      getVaultById();
      getKeepsByVaultId();
    });
    return {
      editable,
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      img: computed(() => `url(${AppState.activeVault?.img})`),
      async deleteVault(id) {
        try {
          const yes = await Pop.confirm("Delete Vault?")
          if (yes) {
            await vaultsService.deleteVault(id)
            await vaultsService.getAccountVaults()
            router.push({ name: 'Account' })
          }
        } catch (error) {
          Pop.error(error, ('[deletingVault]'))
        }
      },
      async editVault() {
        try {

          await vaultsService.editVault(editable.value, this.vault.id)
          Modal.getOrCreateInstance('#editVault').hide()
          await vaultsService.getAccountVaults()
          editable.value = ({})
        } catch (error) {
          Pop.error(error, ("[editingVault]"))
        }
      }
    };
  },
  components: { KeepCard }
}
</script>


<style lang="scss" scoped>
.vault-image {
  border-radius: 5px;
  background-image: v-bind(img);
  background-size: cover;
  background-position: center;
  height: 33vh;
  width: 40vw
}

.vault-title {
  display: flex;
  flex-direction: column;
  justify-content: baseline;
  align-items: center;
  color: aliceblue;
  text-shadow: 1px 1px 2px rgb(6, 6, 6);
  flex-grow: 10 10;
}

.growBoy {
  flex-direction: column;
  height: 18vh;
}

.masonry-with-columns {
  columns: 4;
  column-gap: 1rem;


  img.photo {
    width: 20vw;
    margin-top: 1.5rem
  }
}

.keep {
  background-color: #DED6E9;
  border-radius: 3rem;
}



@media screen and (max-width: 768px) {
  .masonry-with-columns {
    columns: 2;
    margin-left: 2rem;
    margin-right: 2rem;

  }
}

.delete-button {
  position: sticky;
  bottom: 80vh;
  left: 90vw;
}

.modal-input {
  font-size: small;
  color: rgb(155, 155, 155);
}

.checkbox ::before {
  color: #CCCCCC;
}

.submit-btn {
  color: #8f8f8f
}

.modal-body {
  background-color: #E5E5E5;
  border-top-style: hidden;
  border-right-style: hidden;
  border-left-style: hidden;
  border-bottom-style: solid;

}

.form-control:focus {
  border-color: none;
  box-shadow: none;
  background-color: #E5E5E5;
}

textarea {
  background-color: #E5E5E5;
  border-top-style: hidden;
  border-right-style: hidden;
  border-left-style: hidden;
  border-bottom-style: solid;
}

.modal-head {
  background-color: #E5E5E5;
}

.b-submit {
  width: 12vw;
}

@media screen and (max-width: 768px) {
  .b-submit {
    width: 40vw
  }
}
</style>