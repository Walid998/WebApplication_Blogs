import axios from 'axios'

const getAPI = axios.create({
    baseURL:"https://localhost:44317/api",
})

export
{
    getAPI
}