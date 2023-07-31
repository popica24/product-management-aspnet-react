import axios from "axios"
import authHeader from "./authHeader"

const API_URL = "https://localhost:7207/api/Categories"

 const GetAll = async (offset=0,limit=100) => {

  return axios.get(API_URL+`/${offset}/${limit}`,{
    headers:authHeader()
  })
   
}

const GetById = async (id) => {
   return axios.get(API_URL+`/${id}`,{
    headers:authHeader()
   })
}

const Delete = async (id) => {
   return axios.delete(API_URL+`/${id}`,{
    headers:authHeader()
   })
}

 const Filter = async(categoryName, offset=0,limit=100,orderBy,groupBy) => {

    return axios.get(API_URL+`/filter/${offset}/${limit}`,{
      headers:authHeader(),
      params : {
        Name : categoryName,
        orderBy: orderBy,
        groupBy: groupBy,
        offset: offset,
        limit: limit
      }
    })
    };

    const Post = async (name, description) => {
      console.log(name + description);
      return axios.post(API_URL, {
        Name: name,
        Description: description
      }, {
        headers: authHeader()
      });
    };
    
    const LoadProducts = async (id,offset,limit)=>{
      return axios.get(API_URL+`/${id}/products/${offset}/${limit}`,{
        headers: authHeader()
      });
    };

    const Put = async (products,name,description,categoryId) => {
      console.log(categoryId);
      return axios.put(API_URL+`/${categoryId}`,{
        Name : name,
        Description : description,
        Products : products,
        id : categoryId
      },{
        headers : authHeader()
      })
    }
    const DeleteProduct = async (id,productId)=>{
      return axios.delete(API_URL+`/${id}/${productId}`,{
        headers : authHeader()
      })
    }
    const categoryService = {
      GetAll,
      GetById,
      Delete,
      Filter,
      Post,
      LoadProducts,
      Put,
      DeleteProduct
    }
export default categoryService

