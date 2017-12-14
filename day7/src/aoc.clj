(ns aoc
    (:require [aoc.balancedTree :as bt] )
    (:require [aoc.readFile :as rf] )
)
    
(defn -main [inputFile]
    (def data (rf/read-data inputFile))
    (def tree (bt/make-tree data))
    (println (bt/find-bottom tree))
)