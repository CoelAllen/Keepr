import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService{
  async getProfileVaults(profileId){
    const res = await api.get('api/profiles/' + profileId + '/vaults')
   AppState.vaults = res.data
   logger.log(res.data)
 
  }
  async getKeepsByVaultId(vaultId){
   const res = await api.get('api/vaults/' + vaultId + '/keeps')
    AppState.vaultKeeps = res.data
    console.log(res.data, "gettingVaultKeeps");
  }

  async getVaultById(vaultId){
    const res = await api.get('api/vaults/' + vaultId)
    AppState.activeVault = res.data
  }
  async createVault(data){
    const res = await api.post('api/vaults', data)
    logger.log(res.data, "creatingVault")
    AppState.vaults.push(new Vault(res.data))
  }
  async getAccountVaults(){
    const res = await api.get('/account/vaults')
    logger.log(res.data, "gettingAccountVault")
    AppState.myVaults = res.data.map((v)=> new Vault(v))
  }
  async deleteVault(vaultId){
    const res = await api.delete('api/vaults/' + vaultId)
    AppState.vaults = AppState.vaults.filter((v=> v.id != vaultId))
  }
  async editVault(data, vaultId){
    const res = await api.put('api/vaults/' + vaultId, data)
    logger.log(res.data, "editingVault")
    AppState.activeVault = new Vault(res.data)
  }
}

export const vaultsService = new VaultsService()