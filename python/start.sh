echo 'start.sh'

BASEDIR=$(dirname $0)
source ${BASEDIR}/py3env/bin/activate

JSONPATH="$(dirname "$BASEDIR")"/GraphData/inheritance_graph.json
python ${BASEDIR}/display_graph.py $JSONPATH