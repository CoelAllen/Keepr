<template>
  <form @submit.prevent="createVault()">
    <div class="modal fade" id="createVaultModal" tabindex="-1" aria-labelledby="createVaultModalLabel"
      aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="d-flex justify-content-between p-3 modal-head">
            <h1 class="modal-title fs-5" id="createVaultModalLabel">Add Your Vault</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-head">
            <div>
              <div class="form-floating mb-3">
                <input type="text" v-model="editable.name" class="modal-body form-control" id="vaultTitleInput"
                  placeholder="Title...">
                <label for="vaultTitleInput" class="modal-input">Title</label>
              </div>
              <div class="form-floating mb-3">
                <input type="url" v-model="editable.img" class="modal-body form-control" id="vaultImageInput"
                  placeholder="Image URL...">
                <label for="vaultImageInput" class="modal-input">Image URL...</label>
              </div>
              <div class="form-floating mb-3">
                <input type="text" v-model="editable.description" class="modal-body form-control" id="vaultImageInput"
                  placeholder="Description...">
                <label for="vaultImageInput" class="modal-input">Description...</label>
              </div>
            </div>
          </div>
          <div class="text-end pb-3 pe-3 modal-head">
            <small class="modal-input">Private Vaults can only be seen by you</small>
            <div class="me-5 mb-2">
              <input v-model="editable.isPrivate" class="form-check-input me-1" type="checkbox" value=""
                id="privateCheck">
              <label class="form-check-label check-outline" for="privateCheck">
                Make Vault Private?
              </label>
            </div>
            <button type="submit" class="btn btn-dark b-submit me-4">Create Vault</button>
          </div>
        </div>
      </div>
    </div>
  </form>
</template>


<script>
import { Modal } from 'bootstrap';
import { ref } from 'vue';
import { vaultsService } from '../services/VaultsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createVault() {
        try {
          await vaultsService.createVault(editable.value)
          editable.value = {}
          Modal.getOrCreateInstance('#createVaultModal').hide()
        } catch (error) {
          Pop.error(error.message, ('[creatingVault]'))
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.modal-input {
  font-size: small;
  color: rgb(155, 155, 155);
}

.checkbox ::before {
  color: #CCCCCC;
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
</style>