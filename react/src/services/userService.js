import axios from "axios"
const API_URL = 'https://localhost:7207/api/Authorization/'
const login = async (email,password)=>{    
     return axios.post(API_URL + "login",{
      email,
      password
     }).then((response)=>{
      console.log(response.data);
      if(response.data.token){
         localStorage.setItem("user",JSON.stringify(response.data))
      }
     })   
 }

const register = async(email,username,password)=>{
   return axios.post(API_URL+"signup",{
      username,
      email,
      password
   })
 }

const logout = async () => {
    localStorage.removeItem("user");
 }

const getCurrentUser = () => {
   return JSON.parse(localStorage.getItem("user"));
 }

 const userService = {
   register,
   login,
   logout,
   getCurrentUser
 };

 export default userService;