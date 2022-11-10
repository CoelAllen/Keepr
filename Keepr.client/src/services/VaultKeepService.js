import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService{
async createVaultKeep(data, keepId){
  data.keepId = keepId
  const res = await api.post('api/vaultkeeps', data)
  // res.data.keepId = keepId
  logger.log(res.data, "creating vaultKeep")
  AppState.vaultKeeps.push(res.data)
  
}
async deleteVaultKeep(id){
  const res = await api.delete('api/vaultkeeps/' + id)
  AppState.vaultKeeps = AppState.vaultKeeps.filter((v)=> v.id != id)
}
}
export const vaultKeepsService = new VaultKeepsService()