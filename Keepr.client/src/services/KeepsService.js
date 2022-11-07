import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getAllKeeps(){
    const res = await api.get('api/keeps')
// NOTE Have to set up appstate and model before I can data dump
    logger.log('[get all keeps]', res.data)
  }

}
export const keepsService = new KeepsService()