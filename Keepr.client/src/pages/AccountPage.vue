<template>

  <div class="row  top-row">
    <div class="mt-3 col-md-12 d-flex justify-content-center top-row">
      <img class="account-background img-fluid rounded" :src="account.coverImg" alt="">
      <img class="account-image" :src="account.picture" alt="">
    </div>
  </div>
  <div class="row">
    <div class="col-md-12  text-center mt-5 ">
      <h3>{{ account.name }}
        <i class="mdi mdi-account-details-outline selectable fs-5" title="Edit Account Information"
          data-bs-toggle="modal" data-bs-target="#accountModal"></i>
      </h3>
      <p>{{ vaults?.length }} Vaults | {{ keeps.length }} Keeps</p>
    </div>
  </div>
  <div class="row justify-content-center">
    <div class="col-md-8 ms-3">
      <h1>Vaults</h1>
    </div>
  </div>
  <div class="row justify-content-center">
    <div class="col-md-8 d-flex cards flex-wrap vaults">
      <VaultCard :key="v.id" :vault="v" v-for="v in vaults" />
    </div>


  </div>
  <div class="row justify-content-center">
    <div class="col-md-8 ms-3 vault">
      <h1>Keeps</h1>
    </div>
  </div>
  <div class="row justify-content-center">
    <div class="col-md-8 masonry-with-columns top-row mt-2">
      <KeepCard :key="k.id" :keep="k" v-for="k in keeps" />
    </div>
  </div>
  <!-- <button type="button" class="btn btn-primary editButton" data-bs-toggle="modal" data-bs-target="#accountModal">
    Edit Account
  </button> -->

  <!-- Modal -->
  <form @submit.prevent="editAccount()">
    <div class="modal fade" id="accountModal" tabindex="-1" aria-labelledby="accountModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-head d-flex justify-content-between p-3">
            <h1 class="modal-title fs-5" id="accountModalLabel">Edit Account Information</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-head">
            <div class="form-floating mb-3">
              <input type="text" v-model="editable.name" required class="modal-body form-control" id="accountNameInput"
                placeholder="Name...">
              <label for="accountNameInput" class="modal-input">Name</label>
            </div>
            <div class="form-floating mb-3">
              <input type="url" v-model="editable.picture" required class="modal-body form-control" id="accountImgInput"
                placeholder="Image URL...">
              <label for="accountImageInput" class="modal-input">Image URL...</label>
            </div>
            <div class="form-floating mb-3">
              <input type="url" v-model="editable.coverImg" required class="modal-body form-control" id="coverImgInput"
                placeholder="Cover Image URL...">
              <label for="coverImageInput" class="modal-input">Cover Image URL...</label>
            </div>
          </div>
          <div class="modal-footer modal-head">
            <button type="button" class="btn" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-dark b-submit">Save changes</button>
          </div>
        </div>
      </div>
    </div>
  </form>
</template>
  

<script>
import { Modal } from 'bootstrap'
import { computed, onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import KeepCard from '../components/KeepCard.vue'
import VaultCard from '../components/VaultCard.vue'
import { accountService } from '../services/AccountService.js'
import { keepsService } from '../services/KeepsService.js'
import Pop from '../utils/Pop.js'
export default {
  setup() {
    const route = useRoute();
    const editable = ref({})
    // async function getAccountVaults() {
    //   try {
    //     await vaultsService.getAccountVaults();
    //   }
    //   catch (error) {
    //     Pop.error(error, "[getAccountVaults]");
    //   }
    // }
    // async function getAccountKeeps() {
    //   try {
    //     await keepsService.getAccountKeeps(AppState.account.id)
    //   } catch (error) {
    //     Pop.error(error, "[getAccountKeeps]")
    //   }
    // }
    // onMounted(() => {
    //   getAccountVaults();
    //   getAccountKeeps()
    // });
    return {
      editable,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults),
      keeps: computed(() => AppState.myKeeps),
      async editAccount() {
        try {
          await accountService.editAccount(editable.value)
          editable.value = {}
          Modal.getOrCreateInstance('#accountModal').hide()
        } catch (error) {
          Pop.error(error, ("[editingAccount]"))
        }
      }
    };
  },
  components: { VaultCard, KeepCard }
}
</script>

<style lang="scss" scoped>
.account-image {
  border-radius: 50%;
  height: 10vh;

  position: absolute;
  top: 40vh;
  border-color: aliceblue;
  border-width: 2px;
  border-style: solid;
  box-shadow: 0px 3px 1px 2px rgba(128, 128, 128, 0.584);
}



.account-background {
  height: 40vh;
  width: auto;
}

.masonry-with-columns {
  columns: 4;
  column-gap: 1rem;

  img.photo {
    width: 20vw;
    margin-top: 1.5rem;
  }
}

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

.modal-head {
  background-color: #E5E5E5;
}

.editButton {
  position: sticky;
  bottom: 10vh;
  left: 90vw;
}

@media screen and (max-width: 768px) {
  .masonry-with-columns {
    columns: 2;
    margin-left: 1rem;
    margin-right: 1rem;

  }



  .editButton {
    position: sticky;
    bottom: 13vh;
    left: 63vw;
  }

  .cards {
    columns: 2;
    margin-right: 2rem;
    margin-left: 2rem;
  }

  .account-image {
    border-radius: 50%;
    height: 10vh;

    position: absolute;
    top: 35vh;
    border-color: aliceblue;
    border-width: 2px;
    border-style: solid;
    box-shadow: 0px 3px 1px 2px rgba(128, 128, 128, 0.584);


  }

  .vaults {
    padding: 0;
    margin: 0;
  }

  .top-row {
    margin: 0;
    padding: 0;
  }

}
</style>
