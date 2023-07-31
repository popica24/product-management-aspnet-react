import axios from "axios";
import authHeader from "./authHeader";
const API_URL = "https://localhost:7207/api/Products"

const GetAll = async (offset= 0,limit=100) => {
    return axios.get(API_URL+`/${offset}/${limit}`,{
        headers : authHeader()
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
 const Filter = async(productName,maxQuantity,minQuantity,CategoryId, offset=0,limit=100,orderBy,groupBy) => {

    return axios.get(API_URL+`/filter/${offset}/${limit}`,{
      headers:authHeader(),
      params : {
        Name : productName,
        minQuantity : minQuantity,
        maxQuantity : maxQuantity,
        CategoryId : CategoryId,
        orderBy: orderBy,
        groupBy: groupBy,
        offset: offset,
        limit: limit
      }
    })
    };
    const Post = async (name, description,quantity) => {
      console.log(name + description + quantity);
        return axios.post(API_URL,
          {
            Name : name,
            Description : description,
            Quantity : quantity
          
        },{
          headers:authHeader(),
        })
      };
      const LoadCategories = async (id) => {
        return axios.get(API_URL+`/${id}/categories`,{
          headers:authHeader()
        })
      }
const productService = {
    GetAll,
    GetById,
    Delete,
    Filter,
    Post,
    LoadCategories
}
export default productService