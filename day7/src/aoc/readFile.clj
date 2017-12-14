(ns aoc.readFile
    (:require [clojure.java.io :as io] )
)

(defn read-data [inputFile]
    (def data-file (io/reader inputFile))
    (slurp data-file)
)
