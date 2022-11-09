<template>
  <form @submit.prevent="createKeep()">
    <div class="modal fade" id="createKeepModal" tabindex="-1" aria-labelledby="createKeepModalLabel"
      aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="d-flex justify-content-between p-3 modal-head">
            <h1 class="modal-title fs-5" id="createKeepModalLabel">Add your keep</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-head p-3">
            <div>
              <div class="form-floating mb-3">
                <input type="text" v-model="editable.name" required class="modal-body form-control" id="keepTitleInput"
                  placeholder="Title...">
                <label for="keepTitleInput" class="modal-input">Title</label>
              </div>
              <div class="form-floating mb-3">
                <input type="url" v-model="editable.img" required class="modal-body form-control" id="keepImageInput"
                  placeholder="Image URL...">
                <label for="keepImageInput" class="modal-input">Image URL...</label>
              </div>
            </div>
            <div class="growBoy"></div>
            <div>
              <div class="form-floating mb-3">
                <input type="text" v-model="editable.description" required class="modal-body form-control"
                  id="keepDescriptionInput" placeholder="Description...">
                <label for="keepDescriptionInput" class="modal-input">Description...</label>
              </div>

            </div>

          </div>
          <div class="modal-footer modal-head">
            <button type="submit" class="btn btn-dark">Create</button>
          </div>
        </div>
      </div>
    </div>

  </form>
</template>


<script>
import { Modal } from 'bootstrap';
import { ref } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          await keepsService.createKeep(editable.value)
          editable.value = {}
          Modal.getOrCreateInstance('#createKeepModal').hide()
        } catch (error) {
          Pop.error(error.message, ('[creatingKeep]'))
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

.growBoy {
  flex-direction: column;
  height: 18vh;
}
</style>