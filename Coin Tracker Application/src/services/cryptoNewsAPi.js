import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';

const cryptoNewsHeaders = {
    
    'X-RapidAPI-Key': '19faf20c7cmsh60b69bc01cba69cp14569cjsn2c7f5a041d51',
    'X-RapidAPI-Host': 'cryptocurrency-news2.p.rapidapi.com'
      }
   
const baseUrl = 'https://cryptocurrency-news2.p.rapidapi.com/v1';


const createRequest = (url) => ({url, headers: cryptoNewsHeaders});


export const cryptoNewsApi = createApi({
    reducerPath: 'cryptoNewsApi',
    baseQuery: fetchBaseQuery({ baseUrl}),
    endpoints: (builder) => ({
        getCryptoNews: builder.query({
            query: () => createRequest('/coindesk'),
        })
    })
});

export const {
    useGetCryptoNewsQuery,
    
} = cryptoNewsApi;
