const Pool = require('pg').Pool
const pool = new Pool({
  user: 'postgres',
  host: 'localhost',
  database: 'postgres',
  password: 'Saibaba.123',
  port: 5432,
})


const getCars = (request, response) => {
  pool.query("select cars.id, cars.car_name, model.model_name, make.make_name from ((cars inner join model on cars.model_id=model.id) inner join make on cars.make_id = make.id) Order by cars.id ASC", (error, results) => {
    if (error) {
      console.log("Error")
    }
    response.status(200).json(results.rows)
  })
}


const getCarById = (request, response) => {
  const id = parseInt(request.params.id)

  pool.query('select cars.id, cars.car_name, model.model_name, make.make_name from ((cars inner join model on cars.model_id=model.id) inner join make on cars.make_id = make.id) where cars.id = $1 ', [id], (error, results) => {
    if (error) {
        response.json({ info: 'This Id is Not Exist' })
      throw error
    }
    response.status(200).json(results.rows)
  })
}



module.exports = {
  getCars,
  getCarById,
  
  
}
