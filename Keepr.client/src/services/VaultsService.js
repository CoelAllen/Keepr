import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService{
  async getProfileVaults(profileId){
    const res = await api.get('api/profiles/' + profileId + '/vaults')
   AppState.vaults = res.data
 
  }
  async getKeepsByVaultId(vaultId){
    debugger
   const res = await api.get('api/vaults/' + vaultId + '/keeps')
    AppState.vaultKeeps = res.data.map((k)=> new Keep(k))
    console.log(res.data, "gettingVaultKeeps");
  }

  async getVaultById(vaultId){
    const res = await api.get('api/vaults/' + vaultId)
    AppState.activeVault = res.data
  }
}

export const vaultsService = new VaultsService()