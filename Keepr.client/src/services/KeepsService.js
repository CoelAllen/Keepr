import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getAllKeeps(){
    const res = await api.get('api/keeps')
// NOTE Have to set up appstate and model before I can data dump
    logger.log('[get all keeps]', res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async getProfileKeeps(profileId){
    const res = await api.get('api/profiles/' + profileId + '/keeps')
    AppState.keeps = res.data.map((k) => new Keep(k))
  }
  async getAccountKeeps(accountId){
    const res = await api.get('api/profiles/' + accountId + '/keeps')
    AppState.myKeeps = res.data
    logger.log(res.data)
  }
  async removeKeep(id){
    api.delete('api/keeps/' + id)
    AppState.myKeeps = AppState.myKeeps.filter(k=> k.id != id)
  }
  async createKeep(data){
    const res = await api.post('api/keeps', data)
    logger.log(res.data)
    AppState.keeps.push(res.data)
    AppState.myKeeps.push(res.data)
  }
  async getKeepById(keepId){
    const res = await api.get('api/keeps/' + keepId)
    AppState.activeKeep = res.data
    logger.log("gettingKeepById", res.data)
  }
}
export const keepsService = new KeepsService()