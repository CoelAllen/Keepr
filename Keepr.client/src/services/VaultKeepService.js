import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService{
async createVaultKeep(data){
  const res = await api.post('api/vaultkeeps', data)
  logger.log(res.data, "creating vaultKeep")
  AppState.vaultKeeps.push(res.data)
  
}
}
export const vaultKeepsService = new VaultKeepsService()