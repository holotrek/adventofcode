(ns aoc
    (:require [aoc.balancedTree :as bt] )
    (:require [aoc.readFile :as rf] )
)
    
(defn -main [part inputFile]
    (def data (rf/read-data inputFile))
    (def tree (bt/make-tree data))
    (if (= part "1")
        (println (bt/find-bottom tree))
        (println (bt/find-unbalance tree))
    )
)